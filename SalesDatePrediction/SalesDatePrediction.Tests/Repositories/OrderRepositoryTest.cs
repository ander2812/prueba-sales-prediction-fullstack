using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Infrastructure.Data;
using SalesDatePrediction.Infrastructure.Repositories;

public class OrderRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly OrderRepository _repository;

    public OrderRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "OrderTestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new OrderRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var orders = new[]
        {
            new Order { orderid = 1, custid = 1, shipname = "Ship A" },
            new Order { orderid = 2, custid = 1, shipname = "Ship B" },
            new Order { orderid = 3, custid = 2, shipname = "Ship C" }
        };
        _context.Orders.AddRange(orders);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetOrdersByCustomerIdAsync_ShouldReturnCorrectOrders()
    {

        var customerId = 1;

        var result = await _repository.GetOrdersByCustomerIdAsync(customerId);

        Assert.Equal(2, result.Count());
        Assert.All(result, o => Assert.Equal(customerId, o.custid));
    }

    [Fact]
    public async Task AddOrderWithDetailsAsync_ShouldAddOrderAndReturnId()
    {

        var newOrder = new Order { custid = 3, shipname = "New Ship" };

        var newOrderId = await _repository.AddOrderWithDetailsAsync(newOrder);

        var addedOrder = await _context.Orders.FindAsync(newOrderId);
        Assert.NotNull(addedOrder);
        Assert.Equal("New Ship", addedOrder.shipname);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllOrders()
    {

        var result = await _repository.GetAllAsync();

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOrderFromDatabase()
    {

        await _repository.DeleteAsync(1);

        var deletedOrder = await _context.Orders.FindAsync(1);
        Assert.Null(deletedOrder);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}