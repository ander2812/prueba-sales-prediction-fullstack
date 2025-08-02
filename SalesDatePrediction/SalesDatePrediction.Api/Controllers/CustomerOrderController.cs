using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Services;

namespace SalesDatePrediction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerOrderController: ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpGet("salespredictions")]
        public async Task<ActionResult<List<SalesPredictionDto>>> GetSalesPredictions()
        {
            var predictions = await _customerOrderService.GetSalesPredictionsAsync();

            return Ok(new
            {
                data = predictions,
                total = predictions.Count()
            });
        }

    }
}
