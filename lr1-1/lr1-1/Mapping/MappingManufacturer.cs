using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace lr1_1.Mapping
{
    public class MappingManufacturer : Profile
    {
        public MappingManufacturer()
        {
            CreateMap<Manufacturer, ManufacturerDto>()
                .ForMember(g => g.NameManufacturer,
                opt => opt.MapFrom(x => string.Join(' ', x.NameManufacturer))); ;
        }
    }
}
