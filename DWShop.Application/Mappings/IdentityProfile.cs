using AutoMapper;
using DWShop.Application.Features.Identity.Commands.Register;
using DWShop.Application.Responses.Identity;
using Microsoft.AspNetCore.Identity;

namespace DWShop.Application.Mappings
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<RegisterUserCommand, IdentityUser>().ReverseMap();
            CreateMap<LoginResponse, IdentityUser>().ReverseMap();
        }
    }
}
