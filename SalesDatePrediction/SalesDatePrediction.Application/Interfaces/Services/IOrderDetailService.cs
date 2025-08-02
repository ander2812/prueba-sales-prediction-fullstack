using SalesDatePrediction.Application.Dtos;

namespace SalesDatePrediction.Application.Interfaces.Services
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailDto>> GetAllAsync();
        Task<OrderDetailDto?> GetByIdAsync(int id);
        Task AddAsync(OrderDetailDto dto);
        Task UpdateAsync(OrderDetailDto dto);
        Task DeleteAsync(int id);
    }
}
