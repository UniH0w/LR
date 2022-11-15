using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace ShopSmarfone.Mapping
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
            CreateMap<Product, ProductDto>();
            CreateMap<Storage, StorageDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<BuyerForCreationDto, Buyer>();
            CreateMap<BuyerForUpdateDto, Buyer >();
            CreateMap<OrderForCreationDto, Order>();
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<StorageForCreationDto, Storage>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<OrderForUpdateDto, Order>().ReverseMap();
            CreateMap<ProdutctForUpdateDto, Product>();
            CreateMap<StorageForUpdateDto, Storage>().ReverseMap();
        }
    }
}
