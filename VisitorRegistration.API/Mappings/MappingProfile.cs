using AutoMapper;
using VisitorRegistration.API.Models;
using VisitorRegistration_Models;

namespace VisitorRegistration.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
