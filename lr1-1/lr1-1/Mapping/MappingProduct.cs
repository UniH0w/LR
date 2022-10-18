using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace lr1_1.Mapping
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(g => g.NameModels,
                opt => opt.MapFrom(x => string.Join(' ', x.NameModels)));
        }


    }

}
