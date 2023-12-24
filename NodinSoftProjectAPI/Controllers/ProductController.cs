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
            if (ModelState.IsValid)
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

                return Unauthorized("please login and then use JWT token");
            }

            return BadRequest(ModelState);
        }



        [HttpGet("GetAllProdcts")]
        public async Task<IActionResult> GetAllProdcts()
        {
            try
            {
                var result = await _mediator.Send(new GetAllProducts.Query());
                result.Products.ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound("Not find any product");
            }
        }







        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(DeleteProductModel deleteProductModel)
        {
            if (ModelState.IsValid)
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
                        return NotFound("Not find any product with this information" );
                    }
                }

                return Unauthorized("please login and then use JWT token");
            }

            return BadRequest(ModelState);
        }



        [HttpGet("GetProductsByUser")]
        public async Task<IActionResult> GetProductsByUser()
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
                        return Ok(result.Products.ToList());
                    }

                    return Ok(result.OperationResult);
                }
                catch (Exception e)
                {
                    return NotFound("Not find any product");
                }
            }

            return Unauthorized("please login and then use JWT token");
        }




        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductModel updateProductModel)
        {
            if (ModelState.IsValid)
            {
                if (User.GetEmail() != null)
                {
                    try
                    {
                        var updateProductCommand = _mapper.Map<UpdateProduct.Command>(updateProductModel);
                        updateProductCommand.EmailUser = User.GetEmail();
                        var result = await _mediator.Send(updateProductCommand);
                        return Ok(result);
                    }
                    catch (Exception e)
                    {
                        return NotFound("Not find any product with this information");
                    }
                }
                return Unauthorized("please login and then use JWT token");
            }

            return BadRequest(ModelState);
        }





        [HttpPost("AddDeletePermission")]
        public async Task<IActionResult> AddDeletePermission(AddDeletePermissionModel addDeletePermissionModel)
        {
            if (ModelState.IsValid)
            {
                if (User.GetEmail() != null)
                {
                    try
                    {
                        var addDeletePermissionToUser = _mapper.Map<AddDeletePermissionToUser.Command>(addDeletePermissionModel);
                      
                        addDeletePermissionToUser.OwnerEmailProduct = User.GetEmail();
                        var result = await _mediator.Send(addDeletePermissionToUser);
                        return Ok(result);
                    }
                    catch (Exception e)
                    {
                        return NotFound("Not find any product with this information");
                    }
                }

                return Unauthorized("please login and then use JWT token");
            }

            return BadRequest(ModelState);
        }


        [HttpPost("AddEditPermission")]
        public async Task<IActionResult> AddEditPermission(AddEditPermissionModel addEditPermissionModel)
        {
            if (ModelState.IsValid)
            {
                if (User.GetEmail() != null)
                {
                    try
                    {
                        var addEditPermissionToUser = _mapper.Map<AddEditPermissionToUser.Command>(addEditPermissionModel);

                        addEditPermissionToUser.OwnerEmailProduct = User.GetEmail();
                        var result = await _mediator.Send(addEditPermissionToUser);
                        return Ok(result);
                    }
                    catch (Exception e)
                    {
                        return NotFound("Not find any product with this information");
                    }
                }

                return Unauthorized("please login and then use JWT token");
            }

            return BadRequest(ModelState);
        }



    }
}