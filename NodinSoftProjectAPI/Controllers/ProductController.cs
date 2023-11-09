using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Services.ProductService;
using NodinSoftProject.Application.Services.ProductService.Enums;
using NodinSoftProjectAPI.Extensions;
using NodinSoftProjectAPI.Models;

namespace NodinSoftProjectAPI.Controllers
{

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

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductModel createProductModel)
        {
            if (User.GetEmail() != null)
            {
                try
                {
                    var prouct = _mapper.Map<CreateProduct.Command>(createProductModel);
                    prouct.EmailUser = User.GetEmail();
                    var result = await _mediator.Send(prouct);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }

            return Unauthorized();
        }



        [HttpGet("GetAllProdcts")]
        public async Task<IActionResult> GetAllProdcts()
        {
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







        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(DeleteProductModel deleteProductModel)
        {
            if (User.GetEmail() != null)
            {
                try
                {
                    var deleteProductCommand = _mapper.Map<DeleteProduct.Command>(deleteProductModel);
                    deleteProductCommand.EmailUser = User.GetEmail();
                    var result = await _mediator.Send(deleteProductCommand);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }

            return Unauthorized();
        }




        [HttpGet("GetProductsByID")]
        public async Task<IActionResult> GetProductsByID()
        {
            if (User.GetEmail() != null)
            {
                try
                {

                    var result = await _mediator.Send(new GetProductsByID.Query()
                    {
                        EmailUser = User.GetEmail()
                    });

                    if (result.OperationResult == OperationResult.Success)
                    {
                        return Ok(result.Products);
                    }

                    return Ok(result.OperationResult);
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }

            return Unauthorized();
        }





        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductModel updateProductModel)
        {
            if (User.GetEmail() != null)
            {
                try
                {
                    var updateProductCommand = _mapper.Map<UpdateProduct.Command>(updateProductModel);
                    var result = await _mediator.Send(updateProductCommand);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }
            return Unauthorized();
        }
    }
}