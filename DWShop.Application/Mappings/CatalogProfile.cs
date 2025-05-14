using AutoMapper;
using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Domain.Entities;

namespace DWShop.Application.Mappings
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<Catalog, CreateCatalogCommand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
