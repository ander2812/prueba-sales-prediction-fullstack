using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Services;

namespace SalesDatePrediction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _service;

        public ShipperController(IShipperService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Shipper = await _service.GetByIdAsync(id);
            return Shipper is null ? NotFound() : Ok(Shipper);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShipperDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.shipperid }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ShipperDto dto)
        {
            if (id != dto.shipperid) return BadRequest();
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
