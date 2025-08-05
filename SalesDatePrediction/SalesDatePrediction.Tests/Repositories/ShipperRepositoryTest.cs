using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Infrastructure.Data;
using SalesDatePrediction.Infrastructure.Repositories;

public class ShipperRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ShipperRepository _repository;

    public ShipperRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ShipperTestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ShipperRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var shippers = new[]
        {
            new Shipper { shipperid = 1, companyname = "Shipper A" },
            new Shipper { shipperid = 2, companyname = "Shipper B" }
        };
        _context.Shippers.AddRange(shippers);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllShippers()
    {
        var result = await _repository.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnCorrectShipper()
    {
        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Shipper A", result.companyname);
    }

    [Fact]
    public async Task AddAsync_ShouldAddShipperToDatabase()
    {

        var newShipper = new Shipper { shipperid = 3, companyname = "Shipper C" };

        await _repository.AddAsync(newShipper);

        var addedShipper = await _context.Shippers.FindAsync(3);
        Assert.NotNull(addedShipper);
        Assert.Equal("Shipper C", addedShipper.companyname);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateShipperInDatabase()
    {

        var shipperToUpdate = await _context.Shippers.FindAsync(1);
        shipperToUpdate.companyname = "Updated Shipper A";

        await _repository.UpdateAsync(shipperToUpdate);

        var updatedShipper = await _context.Shippers.FindAsync(1);
        Assert.Equal("Updated Shipper A", updatedShipper.companyname);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveShipperFromDatabase()
    {

        await _repository.DeleteAsync(1);

        var deletedShipper = await _context.Shippers.FindAsync(1);
        Assert.Null(deletedShipper);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}