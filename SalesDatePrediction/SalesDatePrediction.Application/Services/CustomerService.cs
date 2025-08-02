using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Interfaces.Services;
using SalesDatePrediction.Infrastructure;

namespace SalesDatePrediction.Application.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            return customer is null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task AddAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _repo.AddAsync(customer);
        }

        public async Task UpdateAsync(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _repo.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}
