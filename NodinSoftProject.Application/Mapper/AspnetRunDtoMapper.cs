using AutoMapper;
using NodinSoftProject.Application.DTOs.Account;
using NodinSoftProject.Application.Services.ProductService;
using NodinSoftProject.Domain.Models.Products;
using NodinSoftProject.Domain.Models.User;

namespace NodinSoftProject.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AspnetRunDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class AspnetRunDtoMapper : Profile
    {
        public AspnetRunDtoMapper()
        {
            CreateMap<Product, CreateProduct.Command>().ReverseMap();
            CreateMap<ApplicationUser, RegisterUserDTO>().ReverseMap();
            CreateMap<Product, UpdateProduct.Command>().ReverseMap();
            CreateMap<ApplicationUser, EditProfileDTO>().ReverseMap();
        }
    }
}
