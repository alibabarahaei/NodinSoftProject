using MediatR;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Mapper;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;

namespace NodinSoftProject.Application.Services.ProductService
{
    public class CreateProduct
    {

        public class Command:IRequest<Response>
        {
            public string EmailUser { get; set; }

            public string ProductName { get; set; }

            public string ManufactureEmail { get; set; }

            public string ManufacturePhone { get; set; }

        }

        public class Handler: IRequestHandler<Command, Response>
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
                //TODO
                //var newProduct = ObjectMapper.Mapper.Map<Product>(request);
                //newProduct.Name = request.ProductName;
                //var user = await _userService.GetUserWithEmailAsync(request.EmailUser);
                //newProduct.User = user;
                //await _productRepository.AddEntity(newProduct);
                //await _productRepository.SaveChanges();
                //return new Response()
                //{
                //    OperationResult = OperationResult.Success
                //};
                return new Response();
            }
        }

        public class Response
        {
            public OperationResult OperationResult { get; set; }

        }

    }
}
