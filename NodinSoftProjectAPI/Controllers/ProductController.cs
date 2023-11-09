using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Services.ProductService;
using NodinSoftProjectAPI.Extensions;
using NodinSoftProjectAPI.Models;

namespace NodinSoftProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IUserService userService, IMapper mapper)
        {
            _mediator = mediator;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductModel createProductModel)
        {
            var x=User.GetUserId();
            try
            {
                var prouct = _mapper.Map<CreateProduct.Command>(createProductModel);
                var result = await _mediator.Send(prouct);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [HttpGet(Name = "GetAllProdcts")]
        public async Task<IActionResult> GetAllProdcts()
        {
            var x = User.GetUserId();
            try
            {
                var result = await _mediator.Send(new GetAllProducts.Query());
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
