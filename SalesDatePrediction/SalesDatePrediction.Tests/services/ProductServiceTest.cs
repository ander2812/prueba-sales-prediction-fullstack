using AutoMapper;
using Moq;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Services;
using SalesDatePrediction.Infrastructure;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _mockMapper = new Mock<IMapper>();
        _productService = new ProductService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfProductDtos()
    {
        var fakeProducts = new List<Product>
        {
            new Product { productid = 1, productname = "Product A" },
            new Product { productid = 2, productname = "Product B" }
        };
        var fakeProductDtos = new List<ProductDto>
        {
            new ProductDto { productid = 1, productname = "Product A" },
            new ProductDto { productid = 2, productname = "Product B" }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeProducts);
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>())).Returns(fakeProductDtos);

        var result = await _productService.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnProductDto()
    {
        var fakeProduct = new Product { productid = 1, productname = "Product A" };
        var fakeProductDto = new ProductDto { productid = 1, productname = "Product A" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeProduct);
        _mockMapper.Setup(mapper => mapper.Map<ProductDto>(It.IsAny<Product>())).Returns(fakeProductDto);

        var result = await _productService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.productid);
        Assert.Equal("Product A", result.productname);
        _mockRepo.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }
}