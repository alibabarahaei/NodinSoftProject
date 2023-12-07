using MediatR;
using Microsoft.EntityFrameworkCore;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Mapper;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;
using NodinSoftProject.Domain.Models.ProductUser;

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
            private readonly IGenericRepository<ProductUser> _productUserRepository;

            public Handler(IUserService userService, IGenericRepository<Product> productRepository, IMediator mediator, IGenericRepository<ProductUser> productUserRepository)
            {
                _userService = userService;
                _productRepository = productRepository;
                _mediator = mediator;
                _productUserRepository = productUserRepository;
            }


            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {

                var productUsers = _productUserRepository.GetQuery().Include("Product")
                    .FirstOrDefault(pu => pu.User.Email == request.EmailUser && pu.Product.IsAvailable == true&&pu.DeleteAccess==true);
                    

               
                if (productUsers!=null)
                {
                        var updateProductCommand = ObjectMapper.Mapper.Map<UpdateProduct.Command>(productUsers.Product);
                        updateProductCommand.IsAvailable = false;
                        updateProductCommand.ProductName = productUsers.Product.Name;
                        updateProductCommand.EmailUser = request.EmailUser;
                        updateProductCommand.ProductId = request.ProductId;
                        var result2 = await _mediator.Send(updateProductCommand);
                        if (result2.OperationResult == OperationResult.Success)
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
