using AutoMapper;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Interfaces.Services;

namespace SalesDatePrediction.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repo.GetAllAsync();

            var result = employees.Select(e => new EmployeeDto
            {
                empid = e.empid,
                fulltname = e.firstname + " " + e.lastname,
            });

            return result;
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repo.GetByIdAsync(id);
            return employee is null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task AddAsync(EmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _repo.AddAsync(employee);
        }

        public async Task UpdateAsync(EmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _repo.UpdateAsync(employee);
        }

        public async Task DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}
