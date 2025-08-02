using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;

namespace SalesDatePrediction.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
