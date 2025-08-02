using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _context.Orders.ToListAsync();

        public async Task<Order?> GetByIdAsync(int id) =>
            await _context.Orders.FindAsync(id);

        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is not null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
               return await _context.Orders
                .Where(o => o.custid == customerId)
                .Select(o => new Order
            {
                custid = o.custid,
                orderid = o.orderid,
                requireddate = o.requireddate,
                shippeddate = o.shippeddate,
                shipname = o.shipname,
                shipaddress = o.shipaddress,
                shipcity = o.shipcity
            })
            .ToListAsync();
            }

        public async Task<int> AddOrderWithDetailsAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.orderid;
        }
    }


}
