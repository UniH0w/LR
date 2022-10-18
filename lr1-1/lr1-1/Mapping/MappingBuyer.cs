using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace lr1_1.Mapping
{
    public class MappingBuyer : Profile
    {
        public MappingBuyer()
        {
            CreateMap<Buyer, BuyerDto>()
                .ForMember(g => g.FullName,
                opt => opt.MapFrom(x => string.Join(' ', x.Name, x.Family, x.MiddleName)));
        }

    }
}
