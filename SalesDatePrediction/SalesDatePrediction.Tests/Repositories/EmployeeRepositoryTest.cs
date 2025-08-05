using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Infrastructure.Repositories;
using SalesDatePrediction.Infrastructure.Data;

public class EmployeeRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly EmployeeRepository _repository;

    public EmployeeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeTestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new EmployeeRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var employees = new[]
        {
            new Employee { empid = 1, firstname = "John", lastname = "Doe" },
            new Employee { empid = 2, firstname = "Jane", lastname = "Smith" }
        };
        _context.Employees.AddRange(employees);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEmployees()
    {
        var result = await _repository.GetAllAsync();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, e => e.firstname == "John");
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnCorrectEmployee()
    {
        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("John", result.firstname);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEmployeeToDatabase()
    {
        var newEmployee = new Employee { empid = 3, firstname = "Test", lastname = "User" };

        await _repository.AddAsync(newEmployee);

        var addedEmployee = await _context.Employees.FindAsync(3);
        Assert.NotNull(addedEmployee);
        Assert.Equal("Test", addedEmployee.firstname);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEmployeeInDatabase()
    {
        var employeeToUpdate = await _context.Employees.FindAsync(1);
        employeeToUpdate.firstname = "Updated John";

        await _repository.UpdateAsync(employeeToUpdate);

        var updatedEmployee = await _context.Employees.FindAsync(1);
        Assert.Equal("Updated John", updatedEmployee.firstname);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveEmployeeFromDatabase()
    {
        await _repository.DeleteAsync(1);

        var deletedEmployee = await _context.Employees.FindAsync(1);
        Assert.Null(deletedEmployee);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}