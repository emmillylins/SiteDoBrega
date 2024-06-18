using AutoMapper;
using Domain.Entities;
using WebApi.DTOs;

namespace WebApi.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<CreateUsuarioDto, Usuario>().ReverseMap();
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
        }
    }
}
