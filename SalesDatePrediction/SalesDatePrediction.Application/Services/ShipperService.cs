using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Interfaces.Services;

namespace SalesDatePrediction.Application.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _repo;
        private readonly IMapper _mapper;

        public ShipperService(IShipperRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShipperDto>> GetAllAsync()
        {
            var shippers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ShipperDto>>(shippers);
        }

        public async Task<ShipperDto?> GetByIdAsync(int id)
        {
            var shipper = await _repo.GetByIdAsync(id);
            return shipper is null ? null : _mapper.Map<ShipperDto>(shipper);
        }

        public async Task AddAsync(ShipperDto dto)
        {
            var shipper = _mapper.Map<Shipper>(dto);
            await _repo.AddAsync(shipper);
        }

        public async Task UpdateAsync(ShipperDto dto)
        {
            var shipper = _mapper.Map<Shipper>(dto);
            await _repo.UpdateAsync(shipper);
        }

        public async Task DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}
