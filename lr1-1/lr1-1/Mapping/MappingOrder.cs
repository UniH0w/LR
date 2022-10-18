using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace lr1_1.Mapping
{
    public class MappingOrder : Profile
    {
        public MappingOrder()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(g => g.IdProduct,
                opt => opt.MapFrom(x => string.Join(' ', x.IdProduct1)));
        }
    }
}
