using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Infrastructure;
using SalesDatePrediction.Infrastructure.Data;
using SalesDatePrediction.Infrastructure.Repositories;

public class ProductRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ProductRepository _repository;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ProductTestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ProductRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var products = new[]
        {
            new Product { productid = 1, productname = "Product A" },
            new Product { productid = 2, productname = "Product B" }
        };
        _context.Products.AddRange(products);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProducts()
    {

        var result = await _repository.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnCorrectProduct()
    {

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Product A", result.productname);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProductToDatabase()
    {

        var newProduct = new Product { productid = 3, productname = "Product C" };

        await _repository.AddAsync(newProduct);

        var addedProduct = await _context.Products.FindAsync(3);
        Assert.NotNull(addedProduct);
        Assert.Equal("Product C", addedProduct.productname);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProductInDatabase()
    {
        var productToUpdate = await _context.Products.FindAsync(1);
        productToUpdate.productname = "Updated Product A";

        await _repository.UpdateAsync(productToUpdate);

        var updatedProduct = await _context.Products.FindAsync(1);
        Assert.Equal("Updated Product A", updatedProduct.productname);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveProductFromDatabase()
    {

        await _repository.DeleteAsync(1);

        var deletedProduct = await _context.Products.FindAsync(1);
        Assert.Null(deletedProduct);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}