using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Interfaces.Services;

namespace SalesDatePrediction.Application.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _repo.GetByIdAsync(id);
            return order is null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task AddAsync(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);

            await _repo.AddOrderWithDetailsAsync(order);
        }

        public async Task UpdateAsync(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            await _repo.UpdateAsync(order);
        }

        public async Task DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _repo.GetOrdersByCustomerIdAsync(customerId);
        }
    }
}
