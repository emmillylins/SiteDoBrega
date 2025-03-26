using Application.DTOs;
using Domain.Entities;
using AutoMapper;

namespace WebApi.Config
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<FaixaDTO, Faixa>().ReverseMap();
            CreateMap<CreateFaixaDTO, Faixa>().ReverseMap();

            CreateMap<CategoriaDTO, Categoria>().ReverseMap();
            CreateMap<CreateCategoriaDTO, Categoria>().ReverseMap();
        }
    }
}
