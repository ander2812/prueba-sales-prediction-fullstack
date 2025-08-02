using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderRepository _repository;

        public CustomerOrderService(ICustomerOrderRepository repository)
        {
            _repository = repository;
        }

        public Task<List<SalesPredictionDto>> GetSalesPredictionsAsync()
        {
            return _repository.GetCustomerSalesPredictionsAsync();
        }
    }
}
