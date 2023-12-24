using MediatR;
using Microsoft.EntityFrameworkCore;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;
using NodinSoftProject.Domain.Models.ProductUser;


namespace NodinSoftProject.Application.Services.ProductService
{
    public class GetProductsByID
    {

        public class Query : IRequest<Response>
        {
            public string EmailUser { get; set; }

        }

        public class Handler : IRequestHandler<Query, Response>
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


            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {


                var products = _productUserRepository.GetQuery()
                    .Where(pu => pu.User.Email == request.EmailUser && pu.Product.IsAvailable == true)
                    .Select(pu => pu.Product);

                
                if (products.Any())
                {
                    return new Response()
                    {
                        Products = products,
                        OperationResult = OperationResult.Success
                    };
                }

                return new Response()
                {
                    Products = null,
                    OperationResult = OperationResult.Success
                };
          
            }
        }

        public class Response
        {
            public OperationResult OperationResult { get; set; }
            public IQueryable<Product> Products { get; set; }

        }


    }
}
