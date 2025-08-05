using AutoMapper;
using Moq;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Services;
using SalesDatePrediction.Infrastructure;

public class OrderDetailServiceTests
{
    private readonly Mock<IOrderDetailRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly OrderDetailService _orderDetailService;

    public OrderDetailServiceTests()
    {
        _mockRepo = new Mock<IOrderDetailRepository>();
        _mockMapper = new Mock<IMapper>();
        _orderDetailService = new OrderDetailService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfOrderDetailDtos()
    {
        
        var fakeOrderDetails = new List<OrderDetail>
        {
            new OrderDetail { orderid = 1, productid = 10 },
            new OrderDetail { orderid = 1, productid = 11 }
        };
        var fakeOrderDetailDtos = new List<OrderDetailDto>
        {
            new OrderDetailDto { orderid = 1, productid = 10 },
            new OrderDetailDto { orderid = 1, productid = 11 }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeOrderDetails);
        _mockMapper.Setup(m => m.Map<IEnumerable<OrderDetailDto>>(It.IsAny<IEnumerable<OrderDetail>>())).Returns(fakeOrderDetailDtos);

        
        var result = await _orderDetailService.GetAllAsync();

        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnOrderDetailDto()
    {
        
        var fakeOrderDetail = new OrderDetail { orderid = 1, productid = 10 };
        var fakeOrderDetailDto = new OrderDetailDto { orderid = 1, productid = 10 };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeOrderDetail);
        _mockMapper.Setup(mapper => mapper.Map<OrderDetailDto>(It.IsAny<OrderDetail>())).Returns(fakeOrderDetailDto);

        
        var result = await _orderDetailService.GetByIdAsync(1);

        
        Assert.NotNull(result);
        Assert.Equal(1, result.orderid);
        _mockRepo.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }
}