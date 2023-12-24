using MediatR;
using Microsoft.EntityFrameworkCore;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;
using NodinSoftProject.Domain.Models.ProductUser;

namespace NodinSoftProject.Application.Services.ProductService
{
    public class UpdateProduct
    {


        public class Command : IRequest<Response>
        {

            public string EmailUser { get; set; }

            public long ProductId { get; set; }

            public string ProductName { get; set; }

            public string ManufactureEmail { get; set; }

            public string ManufacturePhone { get; set; }

            public bool IsAvailable { get; set; } = true;

        }

        public class Handler : IRequestHandler<Command, Response>
        {



            private readonly IUserService _userService;
            private readonly IGenericRepository<Product> _productRepository;
            private readonly IGenericRepository<ProductUser> _productUserRepository;

            public Handler(IUserService userService, IGenericRepository<Product> productRepository, IGenericRepository<ProductUser> productUserRepository)
            {
                _userService = userService;
                _productRepository = productRepository;
                _productUserRepository = productUserRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {

                var productUsers = _productUserRepository.GetQuery().Include("Product").Include("User")
                    .FirstOrDefault(pu => pu.User.Email == request.EmailUser && pu.Product.IsAvailable == true && pu.EditAccess == true&& pu.Product.Id==request.ProductId);



              
                if (productUsers != null)
                {
                    productUsers.Product.Name = request.ProductName;
                    productUsers.Product.ManufacturePhone = request.ManufacturePhone;
                    productUsers.Product.ManufactureEmail = request.ManufactureEmail;
                    productUsers.Product.IsAvailable = request.IsAvailable;
                    _productRepository.EditEntity(productUsers.Product);
                    await _productRepository.SaveChanges();
                    return new Response()
                    {
                        OperationResult = OperationResult.Success
                    };

                }
                return new Response()
                {
                    OperationResult = OperationResult.Error
                };
            }
        }

        public class Response
        {
            public OperationResult OperationResult { get; set; }

        }


    }
}
