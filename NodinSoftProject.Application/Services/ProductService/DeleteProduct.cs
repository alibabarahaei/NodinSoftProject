using MediatR;
using Microsoft.EntityFrameworkCore;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Mapper;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;

namespace NodinSoftProject.Application.Services.ProductService
{
    public class DeleteProduct
    {


        public class Command : IRequest<Response>
        {
            public long ProductId { get; set; }

            public string EmailUser { get; set; }

        }

        public class Handler : IRequestHandler<Command, Response>
        {



            private readonly IUserService _userService;
            private readonly IGenericRepository<Product> _productRepository;
            private readonly IMediator _mediator;

            public Handler(IUserService userService, IGenericRepository<Product> productRepository, IMediator mediator)
            {
                _userService = userService;
                _productRepository = productRepository;
                _mediator = mediator;
            }


            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetQuery().Include(p => p.User).FirstOrDefaultAsync(p=>p.IsAvailable==true);

                if (product != null && product.User.Email == request.EmailUser)
                {
                    product.IsAvailable = false;
                    var updateProductCommand = ObjectMapper.Mapper.Map<UpdateProduct.Command>(product);
                    updateProductCommand.ProductName = product.Name;
                    updateProductCommand.EmailUser = request.EmailUser;
                    updateProductCommand.ProductId= request.ProductId;
                    var result = await _mediator.Send(updateProductCommand);
                    if (result.OperationResult == OperationResult.Success)
                    {
                        await _productRepository.SaveChanges();
                        return new Response()
                        {
                            OperationResult = OperationResult.Success
                        };

                    }
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
