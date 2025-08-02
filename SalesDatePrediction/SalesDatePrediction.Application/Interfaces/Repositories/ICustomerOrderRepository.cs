using SalesDatePrediction.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Interfaces.Repositories
{
    public interface ICustomerOrderRepository
    {
        Task<List<SalesPredictionDto>> GetCustomerSalesPredictionsAsync();
    }
}
