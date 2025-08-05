using AutoMapper;
using Moq;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Services;
using SalesDatePrediction.Infrastructure;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _mockRepo = new Mock<IEmployeeRepository>();
        _mockMapper = new Mock<IMapper>();
        _employeeService = new EmployeeService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfEmployeeDtosWithFullNames()
    {
        var fakeEmployees = new List<Employee>
        {
            new Employee { empid = 1, firstname = "John", lastname = "Doe" },
            new Employee { empid = 2, firstname = "Jane", lastname = "Smith" }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeEmployees);

        var result = await _employeeService.GetAllAsync();

        Assert.NotNull(result);
        var employeeDtos = result.ToList();
        Assert.Equal(2, employeeDtos.Count);
        Assert.Equal("John Doe", employeeDtos[0].fulltname);
        Assert.Equal("Jane Smith", employeeDtos[1].fulltname);
        _mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnEmployeeDto()
    {
        var fakeEmployee = new Employee { empid = 1, firstname = "John", lastname = "Doe" };
        var fakeEmployeeDto = new EmployeeDto { empid = 1, fulltname = "John Doe" };

        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeEmployee);
        _mockMapper.Setup(mapper => mapper.Map<EmployeeDto>(It.IsAny<Employee>())).Returns(fakeEmployeeDto);

        var result = await _employeeService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.empid);
        Assert.Equal("John Doe", result.fulltname);
    }

    [Fact]
    public async Task AddAsync_ShouldCallRepositoryAddAsync()
    {
        var employeeDto = new EmployeeDto { empid = 3, fulltname = "Test Employee" };
        var employee = new Employee { empid = 3, firstname = "Test", lastname = "Employee" };
        _mockMapper.Setup(m => m.Map<Employee>(employeeDto)).Returns(employee);

        await _employeeService.AddAsync(employeeDto);

        _mockRepo.Verify(repo => repo.AddAsync(employee), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldCallRepositoryUpdateAsync()
    {
        var employeeDto = new EmployeeDto { empid = 1, fulltname = "Updated Name" };
        var employee = new Employee { empid = 1, firstname = "Updated", lastname = "Name" };
        _mockMapper.Setup(m => m.Map<Employee>(employeeDto)).Returns(employee);

        await _employeeService.UpdateAsync(employeeDto);

        _mockRepo.Verify(repo => repo.UpdateAsync(employee), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallRepositoryDeleteAsync()
    {
        await _employeeService.DeleteAsync(1);

        _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}