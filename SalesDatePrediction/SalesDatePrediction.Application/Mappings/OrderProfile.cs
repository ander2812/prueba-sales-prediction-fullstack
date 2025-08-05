using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;

namespace SalesDatePrediction.Application.Mappings
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.orderid, opt => opt.Ignore());
        }
    }
}
