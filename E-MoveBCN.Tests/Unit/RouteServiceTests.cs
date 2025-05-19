using Microsoft.Extensions.Configuration;
using Moq;
using Repositories.Interface;
using Dto;
using src.Dto.Route;
using src.Entity.Route;
using TestUtils;
using Xunit;
using src.Services;

public class RouteServiceTests
{
    private readonly Mock<IRouteRepository> _routeRepositoryMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IConfiguration> _configMock = new();
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock = new();
    private readonly HttpClient _httpClient;
    private readonly RouteService _routeService;

    public RouteServiceTests()
    {
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _routeService = new RouteService(_configMock.Object, _httpClient, _routeRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task DeleteRoute_ShouldCallRepository()
    {
        // Arrange
        var routeId = Guid.NewGuid().ToString();
        _routeRepositoryMock.Setup(r => r.DeleteRoute(routeId)).ReturnsAsync(true);

        // Act
        var result = await _routeService.DeleteRoute(routeId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeletePublishedRoute_ShouldCallRepository()
    {
        // Arrange
        var routeId = Guid.NewGuid().ToString();
        _routeRepositoryMock.Setup(r => r.DeletePublishedRoute(routeId)).ReturnsAsync(true);

        // Act
        var result = await _routeService.DeletePublishedRoute(routeId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task PublishRoute_ShouldCallRepository()
    {
        // Arrange
        var dto = new PublishedRouteDto();
        _routeRepositoryMock.Setup(r => r.PublishRoute(dto)).ReturnsAsync(true);

        // Act
        var result = await _routeService.PublishRoute(dto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetRoutesNearAsync_ShouldReturnRoutes()
    {
        // Arrange
        var expectedRoutes = new List<PublishedRouteDto> { new PublishedRouteDto(), new PublishedRouteDto() };
        _routeRepositoryMock.Setup(r => r.GetRoutesNearAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                            .ReturnsAsync(expectedRoutes);

        // Act
        var result = await _routeService.GetRoutesNearAsync(41.4, 2.16, 1000);

        // Assert
        Assert.Equal(expectedRoutes.Count, result.Count);
    }
}