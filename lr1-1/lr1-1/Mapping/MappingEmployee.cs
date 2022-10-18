using Entities.DataTransferObjects;
using Entities.Models;
using AutoMapper;

namespace lr1_1.Mapping
{
    public class MappingEmployee : Profile
    {

        public MappingEmployee()
        {
            CreateMap <Employee, EmployeeDto>()
                .ForMember(g => g.CompanyId,
                opt => opt.MapFrom(x => string.Join(' ', x.CompanyId)));
        }
        
    }
}
