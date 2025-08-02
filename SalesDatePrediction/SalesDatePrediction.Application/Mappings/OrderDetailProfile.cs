using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;

namespace SalesDatePrediction.Application.Mappings
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
        }
    }
}
