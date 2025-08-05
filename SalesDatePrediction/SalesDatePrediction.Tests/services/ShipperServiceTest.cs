using AutoMapper;
using Moq;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Services;
using SalesDatePrediction.Infrastructure;

public class ShipperServiceTests
{
    private readonly Mock<IShipperRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ShipperService _shipperService;

    public ShipperServiceTests()
    {
        _mockRepo = new Mock<IShipperRepository>();
        _mockMapper = new Mock<IMapper>();
        _shipperService = new ShipperService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfShipperDtos()
    {
        // Arrange
        var fakeShippers = new List<Shipper>
        {
            new Shipper { shipperid = 1, companyname = "Shipper A" },
            new Shipper { shipperid = 2, companyname = "Shipper B" }
        };
        var fakeShipperDtos = new List<ShipperDto>
        {
            new ShipperDto { shipperid = 1, companyname = "Shipper A" },
            new ShipperDto { shipperid = 2, companyname = "Shipper B" }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeShippers);
        _mockMapper.Setup(m => m.Map<IEnumerable<ShipperDto>>(It.IsAny<IEnumerable<Shipper>>())).Returns(fakeShipperDtos);

        var result = await _shipperService.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnShipperDto()
    {

        var fakeShipper = new Shipper { shipperid = 1, companyname = "Shipper A" };
        var fakeShipperDto = new ShipperDto { shipperid = 1, companyname = "Shipper A" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeShipper);
        _mockMapper.Setup(mapper => mapper.Map<ShipperDto>(It.IsAny<Shipper>())).Returns(fakeShipperDto);

        var result = await _shipperService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.shipperid);
        Assert.Equal("Shipper A", result.companyname);
        _mockRepo.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }
}