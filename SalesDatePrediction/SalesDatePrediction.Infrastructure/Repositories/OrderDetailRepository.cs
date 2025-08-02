using SalesDatePrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces.Repositories;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    internal class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync() =>
            await _context.OrderDetails.ToListAsync();

        public async Task<OrderDetail?> GetByIdAsync(int id) =>
            await _context.OrderDetails.FindAsync(id);

        public async Task AddAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail is not null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
