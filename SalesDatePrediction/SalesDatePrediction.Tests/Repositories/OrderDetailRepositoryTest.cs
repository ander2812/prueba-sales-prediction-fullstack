using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Infrastructure.Data;
using SalesDatePrediction.Infrastructure.Repositories;

public class OrderDetailRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly OrderDetailRepository _repository;

    public OrderDetailRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "OrderDetailTestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new OrderDetailRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var orderDetails = new[]
        {
            new OrderDetail { orderid = 1, productid = 10, unitprice = 10.5m },
            new OrderDetail { orderid = 1, productid = 11, unitprice = 12.0m }
        };
        _context.OrderDetails.AddRange(orderDetails);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllOrderDetails()
    {
        var result = await _repository.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnCorrectOrderDetail()
    {

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(10.5m, result.unitprice);
    }

    [Fact]
    public async Task AddAsync_ShouldAddOrderDetailToDatabase()
    {

        var newOrderDetail = new OrderDetail { orderid = 2, productid = 12, unitprice = 20.0m };

        await _repository.AddAsync(newOrderDetail);

        var addedDetail = await _context.OrderDetails.FindAsync(2);
        Assert.NotNull(addedDetail);
        Assert.Equal(20.0m, addedDetail.unitprice);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateOrderDetailInDatabase()
    {

        var detailToUpdate = await _context.OrderDetails.FindAsync(1);
        detailToUpdate.unitprice = 15.0m;

        await _repository.UpdateAsync(detailToUpdate);

        var updatedDetail = await _context.OrderDetails.FindAsync(1);
        Assert.Equal(15.0m, updatedDetail.unitprice);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOrderDetailFromDatabase()
    {

        await _repository.DeleteAsync(1);

        var deletedDetail = await _context.OrderDetails.FindAsync(1);
        Assert.Null(deletedDetail);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}