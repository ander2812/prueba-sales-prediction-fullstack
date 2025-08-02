using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Interfaces.Services
{
    public interface IShipperService
    {
        Task<IEnumerable<ShipperDto>> GetAllAsync();
        Task<ShipperDto?> GetByIdAsync(int id);
        Task AddAsync(ShipperDto dto);
        Task UpdateAsync(ShipperDto dto);
        Task DeleteAsync(int id);
    }
}
