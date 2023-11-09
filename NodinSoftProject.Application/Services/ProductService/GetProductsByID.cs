using MediatR;
using Microsoft.EntityFrameworkCore;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;


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

            public Handler(IUserService userService, IGenericRepository<Product> productRepository)
            {
                _userService = userService;
                _productRepository = productRepository;
            }


            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = _productRepository.GetQuery().Where(p => (p.User.Email == request.EmailUser&&p.IsAvailable==true)).ToList();
                if (products != null)
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
                    OperationResult = OperationResult.Error
                };
            }
        }

        public class Response
        {
            public OperationResult OperationResult { get; set; }
            public List<Product> Products { get; set; }

        }


    }
}
