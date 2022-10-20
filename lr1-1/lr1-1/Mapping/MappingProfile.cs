using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace lr1_1.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Buyer, BuyerDto>();
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Storage, StorageDto>();
        }
    }
}
