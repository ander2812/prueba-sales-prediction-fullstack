using AutoMapper;
using Moq;
using SalesDatePrediction.Application.Dtos;
using SalesDatePrediction.Application.Interfaces.Repositories;
using SalesDatePrediction.Application.Services;
using SalesDatePrediction.Infrastructure;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockRepo = new Mock<IOrderRepository>();
        _mockMapper = new Mock<IMapper>();
        _orderService = new OrderService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldCallRepositoryAddOrderWithDetailsAsync()
    {
        // Arrange
        var orderDto = new OrderDto { OrderId = 100 };
        var order = new Order { orderid = 100 };
        _mockMapper.Setup(m => m.Map<Order>(orderDto)).Returns(order);

        // Act
        await _orderService.AddAsync(orderDto);

        // Assert
        _mockRepo.Verify(repo => repo.AddOrderWithDetailsAsync(order), Times.Once);
    }

    [Fact]
    public async Task GetOrdersByCustomerIdAsync_ShouldReturnOrdersFromRepository()
    {
        // Arrange
        var customerId = 1;
        var fakeOrders = new List<Order> { new Order { orderid = 1, custid = customerId } };
        _mockRepo.Setup(repo => repo.GetOrdersByCustomerIdAsync(customerId)).ReturnsAsync(fakeOrders);

        // Act
        var result = await _orderService.GetOrdersByCustomerIdAsync(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _mockRepo.Verify(repo => repo.GetOrdersByCustomerIdAsync(customerId), Times.Once);
    }
}