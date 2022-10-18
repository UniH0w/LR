using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace lr1_1.Mapping
{
    public class MappingStorage : Profile
    {
        public MappingStorage()
        {
            CreateMap<Storage, StorageDto>()
                .ForMember(g => g.Quantity,
                opt => opt.MapFrom(x => string.Join(' ',x.Quantity )));
        }
            


    }
}
