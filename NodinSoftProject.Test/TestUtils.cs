//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NodinSoftProject.Application.Services.ProductService;
//using NodinSoftProject.Domain.Models.Products;

//namespace NodinSoftProject.Tests
//{
//    public static class TestUtils
//    {
//        public static readonly CreateProduct.Command TestCreateProductCommand = new ()
//        {
//            ManufactureEmail = "alibabarahaei@gmail.com",
//            ManufacturePhone = "09330807786",
//            Name = "Apple",
//            UserId = "78bb8bfe-e3e2-4149-a58d-05cf7042f298"
//        };

//        public static readonly UpdateProduct.Command TestUpdateProductCommand = new()
//        {
//            ManufactureEmail = "alibabarahaei@gmail.com",
//            ManufacturePhone = "09330807786",
//            Name = "Shiami",
//            IsAvailable = true,
//            ProductId = 1
//        };
//        public static readonly DeleteProduct.Command TestDeleteProductCommand = new ()
//        {
//          ProductId =1 ,
//          UserId = "78bb8bfe-e3e2-4149-a58d-05cf7042f298"
//        };
//        public static readonly GetAllProducts.Query TestGetAllProductsQuery = new()
//        {

//        };
//        public static readonly GetProductsByID.Query TesGetProductsByIDQuery = new()
//        {
//            UserId = "78bb8bfe-e3e2-4149-a58d-05cf7042f298",
//        };
//    }
//}
