using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Interfaces.Services;

namespace SalesDatePrediction.Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _repo;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllAsync()
        {
            var orderDetails = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetails);
        }

        public async Task<OrderDetailDto?> GetByIdAsync(int id)
        {
            var orderDetail = await _repo.GetByIdAsync(id);
            return orderDetail is null ? null : _mapper.Map<OrderDetailDto>(orderDetail);
        }

        public async Task AddAsync(OrderDetailDto dto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(dto);
            await _repo.AddAsync(orderDetail);
        }

        public async Task UpdateAsync(OrderDetailDto dto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(dto);
            await _repo.UpdateAsync(orderDetail);
        }

        public async Task DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}
