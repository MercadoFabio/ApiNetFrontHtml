using AutoMapper;
using Parcial.Dtos;
using Parcial.Models;

namespace Parcial.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AlbanilDto, Albanile>().ReverseMap();
            CreateMap<Obra, ObraDto>()
            .ForMember(x => x.CantidadAlbaniles,
            opt => opt.MapFrom(src => src.AlbanilesXObras.Count))
            .ForMember(x => x.NombreTipoObra,
            opt => opt.MapFrom(src => src.IdTipoObraNavigation.Nombre)).ReverseMap();
            CreateMap<AlbanilesXObra, AlbanilXObraDto>().ReverseMap();
        }
    }
}
