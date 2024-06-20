using AutoMapper;
using Domain.Entities;
using WebApi.DTOs;

namespace WebApi.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<CategoriaDto, Categoria>().ReverseMap();
            CreateMap<CreateCategoriaDto, Categoria>().ReverseMap();

            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<CreateUsuarioDto, Usuario>().ReverseMap();

            CreateMap<FaixaDto, Faixa>().ReverseMap();
            CreateMap<CreateFaixaDto, Faixa>().ReverseMap();
        }
    }
}
