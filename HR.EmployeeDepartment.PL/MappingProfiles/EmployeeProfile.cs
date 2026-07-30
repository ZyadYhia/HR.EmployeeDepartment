using AutoMapper;
using HR.EmployeeDepartment.DAL.Models;
using HR.EmployeeDepartment.PL.DTOs;

namespace HR.EmployeeDepartment.PL.MappingProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
        //CreateMap<CreateEmployeeDto, Employee>().ForMember(e => e.Name, o => o.MapFrom(s => s.EmpName));
        //CreateMap<CreateEmployeeDto, Employee>();
        //CreateMap<Employee, CreateEmployeeDto>();
    }
}
