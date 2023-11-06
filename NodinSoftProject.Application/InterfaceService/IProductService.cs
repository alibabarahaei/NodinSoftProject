using NodinSoftProject.Application.DTOs.Product;
using NodinSoftProject.Domain.Models.Products;

namespace NodinSoftProject.Application.InterfaceService
{
    public interface IProductService : IDisposable
    {
        public Task<ProductResult> AddProductAsync(AddProductDTO addProductDTO);
        public Task<ProductResult> DeleteProductAsync(DeleteProductDTO deleteProductDTO);
        public Task<ProductResult> EditProductAsync(EditProductDTO editProductDTO);
        public Task<List<Product>> GetProductsAsync(GetProductsDTO GetProductsDTO);




    }
}
