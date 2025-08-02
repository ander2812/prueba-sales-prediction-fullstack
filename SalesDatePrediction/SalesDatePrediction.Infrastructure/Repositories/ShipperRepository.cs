using SalesDatePrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces.Repositories;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    internal class ShipperRepository : IShipperRepository
    {
        private readonly ApplicationDbContext _context;

        public ShipperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipper>> GetAllAsync() =>
            await _context.Shippers.ToListAsync();

        public async Task<Shipper?> GetByIdAsync(int id) =>
            await _context.Shippers.FindAsync(id);

        public async Task AddAsync(Shipper shipper)
        {
            _context.Shippers.Add(shipper);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shipper shipper)
        {
            _context.Shippers.Update(shipper);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shipper = await _context.Shippers.FindAsync(id);
            if (shipper is not null)
            {
                _context.Shippers.Remove(shipper);
                await _context.SaveChangesAsync();
            }
        }
    }
}
