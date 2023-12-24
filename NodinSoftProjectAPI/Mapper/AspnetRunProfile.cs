using AutoMapper;
using NodinSoftProject.Application.DTOs.Account;
using NodinSoftProject.Application.Services.ProductService;
using NodinSoftProject.Domain.Models.User;
using NodinSoftProjectAPI.Models;

namespace NodinSoftProjectAPI.Mapper
{
    public class AspnetRunProfile : Profile
    {
        public AspnetRunProfile()
        {
            CreateMap<ApplicationUser, UserRegistrationModel>().ReverseMap();
            CreateMap<CreateProduct.Command, CreateProductModel>().ReverseMap();
            CreateMap<UpdateProduct.Command, UpdateProductModel>().ReverseMap();
            CreateMap<DeleteProduct.Command, DeleteProductModel>().ReverseMap();
            CreateMap<RegisterUserDTO, UserRegistrationModel>().ReverseMap();
            CreateMap<DeleteProduct.Command, DeleteProductModel>().ReverseMap();
            CreateMap<AddDeletePermissionToUser.Command, AddDeletePermissionModel>().ReverseMap();
            CreateMap<AddEditPermissionToUser.Command, AddEditPermissionModel>().ReverseMap();
        }
    }
}
