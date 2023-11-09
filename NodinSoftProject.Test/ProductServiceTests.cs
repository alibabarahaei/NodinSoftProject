using MediatR;
using Moq;
using NodinSoftProject.Application.DTOs.Product;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Services.ProductService;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;
using Xunit;

namespace NodinSoftProject.Tests
{
    public class ProductServiceTests
    {

        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IGenericRepository<Product>> _genericRepositoryProductMock;

        public ProductServiceTests()
        {

            _userServiceMock = new();
            _genericRepositoryProductMock=new ();
            _mediator=new ();

        }


        [Fact]
        public async Task CheckCreateProduct()
        {
            var handler = new CreateProduct.Handler(_userServiceMock.Object, _genericRepositoryProductMock.Object);
            var result = await handler.Handle(TestUtils.TestCreateProductCommand, default);
            Assert.Equal(OperationResult.Success,result.OperationResult);

        }
        [Fact]
        public async Task UpdateProduct()
        {
            var handler = new UpdateProduct.Handler(_userServiceMock.Object, _genericRepositoryProductMock.Object);
            var result = await handler.Handle(TestUtils.TestUpdateProductCommand, default);
            Assert.Equal(OperationResult.Success, result.OperationResult);
        }
        [Fact]
        public async Task DeleteProduct()
        {
            var handler = new DeleteProduct.Handler(_userServiceMock.Object, _genericRepositoryProductMock.Object,_mediator.Object);
            var result = await handler.Handle(TestUtils.TestDeleteProductCommand, default);
            Assert.Equal(OperationResult.Success, result.OperationResult);
        }
        [Fact]
        public async Task GetAllProducts()
        {
            //var handler = new GetAllProducts.Handler(_userServiceMock.Object, _genericRepositoryProductMock.Object);
            //var result = await handler.Handle(TestUtils.TestGetAllProductsQuery, default);
            //Assert.Equal(OperationResult.Success, result.OperationResult);
        }
        [Fact]
        public async Task GetProductsByID()
        {
            var handler = new GetProductsByID.Handler(_userServiceMock.Object, _genericRepositoryProductMock.Object);
            var result = await handler.Handle(TestUtils.TesGetProductsByIDQuery, default);
            Assert.Equal(OperationResult.Success, result.OperationResult);
        }
    }

  
}
