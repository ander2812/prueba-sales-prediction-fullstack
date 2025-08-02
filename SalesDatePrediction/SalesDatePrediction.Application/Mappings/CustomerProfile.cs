using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;

namespace SalesDatePrediction.Application.Mappings
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
