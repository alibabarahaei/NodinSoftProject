using Microsoft.EntityFrameworkCore;
using NodinSoftProject.Application.DTOs.Product;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Mapper;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;

namespace NodinSoftProject.Application.Services
{
    public class ProductService :IProductService
    {





        #region constructor
        private readonly IUserService _userService;
        private readonly IGenericRepository<Product> _productRepository;

        public ProductService(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public async Task<ProductResult> AddProductAsync(AddProductDTO addProductDTO)
        {

            var newProduct = ObjectMapper.Mapper.Map<Product>(addProductDTO);
            var user = await _userService.GetUserWithUserIdAsync(addProductDTO.UserId);
            newProduct.User = user;
            await _productRepository.AddEntity(newProduct);
            await _productRepository.SaveChanges();
            return ProductResult.Success;

        }




        public async Task<ProductResult> DeleteProductAsync(DeleteProductDTO deleteProductDTO)
        {
            var product = await _productRepository.GetQuery().Include(p => p.User).FirstOrDefaultAsync();

            if (product != null && product.User.Id == deleteProductDTO.UserId)
            {
                product.IsAvailable = false;
                var result = await EditProductAsync(ObjectMapper.Mapper.Map<EditProductDTO>(product));
                if (result == ProductResult.Success)
                {
                    await _productRepository.SaveChanges();
                    return ProductResult.Success;

                }
            }
            return ProductResult.Error;
        }





        public async Task<ProductResult> EditProductAsync(EditProductDTO editProductDTO)
        {

            var currentProduct = await _productRepository.GetEntityById(editProductDTO.ProductId);
            currentProduct.Name = editProductDTO.Name;
            currentProduct.ManufacturePhone = editProductDTO.ManufacturePhone;
            currentProduct.ManufactureEmail = editProductDTO.ManufactureEmail;
            currentProduct.IsAvailable = editProductDTO.IsAvailable;
            _productRepository.EditEntity(currentProduct);
            await _productRepository.SaveChanges();
            return ProductResult.Success;

        }


        public async Task<List<Product>> GetProductsAsync(GetProductsDTO GetProductsDTO)
        {
            var products = _productRepository.GetQuery().Include("User").Where(p => (p.User.Id == GetProductsDTO.UserId)).ToList();
            if (products != null)
            {
                return products;
            }

            return null;
        }



        public void Dispose()
        {
            _userService.Dispose();

            if (_productRepository is IDisposable contactRepositoryDisposable)
                contactRepositoryDisposable.Dispose();
            else
                _ = _productRepository.DisposeAsync().AsTask();
        }



    }
}
