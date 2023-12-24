using MediatR;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;
using NodinSoftProject.Domain.Models.ProductUser;
using System;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using NodinSoftProject.Application.Mapper;

namespace NodinSoftProject.Application.Services.ProductService
{
    public class AddEditPermissionToUser
    {
        public class Command : IRequest<Handler.Response>
        {
            public string UserEmail { get; set; }

            public long ProductId { get; set; }

            public bool IsEditPermission { get; set; }

            public string OwnerEmailProduct { get; set; }

        }

        public class Handler : IRequestHandler<Command, Handler.Response>
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

                try
                {


                    var product = await _productRepository.GetQuery().FirstOrDefaultAsync(p=>p.Id==request.ProductId);
                    var user = await _userService.GetUserWithEmailAsync(request.UserEmail);
                    var productUser = await _productUserRepository.GetQuery()
                        .FirstOrDefaultAsync(pu => pu.User.Email == request.UserEmail && pu.ProductId == request.ProductId);





                    if (product!=null&&product.CreatorEmail == request.OwnerEmailProduct)
                    {
                        if (productUser != null)
                        {
                            productUser.EditAccess = request.IsEditPermission;
                            _productUserRepository.EditEntity(productUser);
                            await _productUserRepository.SaveChanges();
                        }
                        else
                        {
                            if (user != null )
                            {
                                var newProductUser = new ProductUser()
                                {
                                    ProductId = request.ProductId,
                                    UserId = user.Id,
                                    DeleteAccess = request.IsEditPermission,
                                    EditAccess = false
                                };
                                await _productUserRepository.AddEntity(newProductUser);
                                await _productUserRepository.SaveChanges();
                            }
                            else
                            {
                                return new Response()
                                {
                                    OperationResult = OperationResult.Error,
                                    MoreInformation = "Email is not found"
                                };
                            }
                        }
                    }
                    else
                    {
                        return new Response()
                        {
                            OperationResult = OperationResult.Error,
                            MoreInformation = "you are not permission to do it"
                        };
                    }

                }
                catch (Exception e)
                {
                    return new Response()
                    {
                        OperationResult = OperationResult.Error,
                        MoreInformation = e.ToString()
                    };
                }

                return new Response()
                {
                    OperationResult = OperationResult.Success,
                    MoreInformation = "operation is successed"
                };

            }


            public class Response
            {
                public OperationResult OperationResult { get; set; }
                public string MoreInformation { get; set; }

            }
        }
    }
}
