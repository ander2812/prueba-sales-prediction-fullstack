using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class CustomerOrderRepository: ICustomerOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalesPredictionDto>> GetCustomerSalesPredictionsAsync()
        {
                var groupedOrders = await _context.Orders
                .Include(o => o.cust)
                .Where(o => o.cust != null)
                .GroupBy(o => new { o.cust!.custid, o.cust.companyname })
                .ToListAsync();

            var result = groupedOrders.Select(g => 
            {
                var orderedDates = g.OrderBy(o => o.orderdate).ToList();

                double averageDaysBetweenOrders = 0;
                if (orderedDates.Count > 1)
                {
                    var differences = orderedDates
                        .Zip(orderedDates.Skip(1), (previous, current) => (current.orderdate - previous.orderdate).TotalDays);
                    averageDaysBetweenOrders = differences.Average();
                }

                var lastOrderDate = orderedDates.Max(o => o.orderdate);

                return new SalesPredictionDto
                {
                    CustId = g.Key.custid,
                    CustomerName = g.Key.companyname,
                    LastOrderDate = lastOrderDate,
                    NextPredictedOrder = lastOrderDate.AddDays(averageDaysBetweenOrders)
                };
            }).ToList();

            return result;
        }
    }
}
