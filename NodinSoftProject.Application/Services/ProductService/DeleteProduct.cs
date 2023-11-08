using MediatR;
using Microsoft.EntityFrameworkCore;
using NodinSoftProject.Application.DTOs.Product;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Mapper;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.Products;

namespace NodinSoftProject.Application.Services.ProductService
{
    public class DeleteProduct
    {


        public class Command : IRequest<Response>
        {
            public long ProductId { get; set; }

            public string UserId { get; set; }

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
                var product = await _productRepository.GetQuery().Include(p => p.User).FirstOrDefaultAsync();

                if (product != null && product.User.Id == request.UserId)
                {
                    product.IsAvailable = false;
                    var result = await _mediator.Send(ObjectMapper.Mapper.Map<UpdateProduct.Command>(product));
                    if (result.ProductResult == ProductResult.Success)
                    {
                        await _productRepository.SaveChanges();
                        return new Response()
                        {
                            ProductResult = ProductResult.Success
                        };

                    }
                }

                return new Response()
                {
                    ProductResult = ProductResult.Error
                };
            }
        }

        public class Response
        {
            public ProductResult ProductResult { get; set; }

        }



    }
}
