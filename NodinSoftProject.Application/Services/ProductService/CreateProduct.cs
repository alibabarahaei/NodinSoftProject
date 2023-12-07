using MediatR;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Mapper;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;
using NodinSoftProject.Domain.Models.ProductUser;

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
            private readonly IGenericRepository<ProductUser> _productUserRepository;
            private readonly IMediator _mediator;

            public Handler(IUserService userService, IGenericRepository<Product> productRepository, IGenericRepository<ProductUser> productUserRepository, IMediator mediator)
            {
                _userService = userService;
                _productRepository = productRepository;
                _productUserRepository = productUserRepository;
                _mediator = mediator;
            }


            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var newProduct = ObjectMapper.Mapper.Map<Product>(request);
                newProduct.Name = request.ProductName;
                var user = await _userService.GetUserWithEmailAsync(request.EmailUser);
                await _productRepository.AddEntity(newProduct);
                await _productRepository.SaveChanges();
                var result = await _mediator.Send(new GetAllProducts.Query());
                var product = result.Products.FirstOrDefault(p =>
                    p.ManufactureEmail == newProduct.ManufactureEmail &&
                    p.ManufacturePhone == newProduct.ManufacturePhone && p.Name == newProduct.Name);

                var newProductUser = new ProductUser()
                {
                    UserId = user.Id,
                    ProductId = product.Id,
                    Product = product,
                    User = user,
                    DeleteAccess = true,
                    EditAccess = true

                };

                await _productUserRepository.AddEntity(newProductUser);
                await _productUserRepository.SaveChanges();
                return new Response()
                {
                    OperationResult = OperationResult.Success
                };
               
            }
        }

        public class Response
        {
            public OperationResult OperationResult { get; set; }

        }

    }
}
