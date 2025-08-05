using SalesDatePrediction.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Interfaces.Services
{
    public interface ICustomerOrderService
    {
        Task<List<SalesPredictionDto>> GetSalesPredictionsAsync(
            int pageIndex,
            int pageSize,
            string sortField,
            string sortOrder,
            string? filterValue);
    }
}
