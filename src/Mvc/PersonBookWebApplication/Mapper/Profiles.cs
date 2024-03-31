using AutoMapper;
using Core.Dtos;
using PersonBookWebApplication.Areas.Per.Models;
using PersonBookWebApplication.Models;
using Utilities.Dtos.AuthenticationApi;

namespace PersonBookWebApplication.Mapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<LoginViewModel, AuthApiLoginRequestDto>();
            CreateMap<RegisterViewModel, AuthApiRegisterRequestDto>();
            CreateMap<PersonViewModel, PersonDto>();
        }
    }
}
