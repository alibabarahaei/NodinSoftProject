using MediatR;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;

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

            public Handler(IUserService userService, IGenericRepository<Product> productRepository)
            {
                _userService = userService;
                _productRepository = productRepository;
            }


            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentProduct = await _productRepository.GetEntityById(request.ProductId);
                if (currentProduct != null)
                {
                    currentProduct.Name = request.ProductName;
                    currentProduct.ManufacturePhone = request.ManufacturePhone;
                    currentProduct.ManufactureEmail = request.ManufactureEmail;
                    currentProduct.IsAvailable = request.IsAvailable;
                    _productRepository.EditEntity(currentProduct);
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
