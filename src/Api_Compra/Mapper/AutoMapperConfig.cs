using Api_Compra.Models;
using AutoMapper;
using Domain.Entidades;

namespace Api_Compra.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fruta, DtoFruta>().ReverseMap();
        }
    }
}
