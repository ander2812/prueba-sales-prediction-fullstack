// src/SalesDatePrediction.Api/Controllers/CustomerOrderController.cs
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Services;

namespace SalesDatePrediction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpGet("salespredictions")]
        public async Task<ActionResult<List<SalesPredictionDto>>> GetSalesPredictions(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery] string sortField,
            [FromQuery] string sortOrder,
            [FromQuery] string? filterValue)
        {
            var predictions = await _customerOrderService.GetSalesPredictionsAsync(
                pageIndex,
                pageSize,
                sortField,
                sortOrder,
                filterValue
            );

            return Ok(new
            {
                data = predictions,
                total = predictions.Count()
            });
        }
    }
}