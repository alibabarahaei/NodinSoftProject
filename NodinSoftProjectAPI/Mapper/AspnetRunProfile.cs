using AutoMapper;
using NodinSoftProject.Application.DTOs.Account;
using NodinSoftProject.Domain.Models.User;
using NodinSoftProjectAPI.Models;

namespace NodinSoftProjectAPI.Mapper
{
    public class AspnetRunProfile : Profile
    {
        public AspnetRunProfile()
        {
            CreateMap<ApplicationUser, UserRegistrationModel>().ReverseMap();
        }
    }
}
