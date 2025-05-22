using Microsoft.Extensions.Configuration;
using Moq;
using Repositories.Interface;
using Dto;
using Dto.Route;
using Entity.Route;
using TestUtils;
using Xunit;
using Services;
namespace E_MoveBCN.Tests.Unit;
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
        this._httpClient = new HttpClient(this._httpMessageHandlerMock.Object);
        this._routeService = new RouteService(this._configMock.Object, this._httpClient, this._routeRepositoryMock.Object, this._userRepositoryMock.Object);
    }

    [Fact]
    public async Task DeleteRoute_ShouldCallRepository()
    {
        // Arrange
        var routeId = Guid.NewGuid().ToString();
        this._routeRepositoryMock.Setup(r => r.DeleteRoute(routeId)).ReturnsAsync(true);

        // Act
        var result = await this._routeService.DeleteRoute(routeId).ConfigureAwait(false);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeletePublishedRoute_ShouldCallRepository()
    {
        // Arrange
        var routeId = Guid.NewGuid().ToString();
        this._routeRepositoryMock.Setup(r => r.DeletePublishedRoute(routeId)).ReturnsAsync(true);

        // Act
        var result = await this._routeService.DeletePublishedRoute(routeId).ConfigureAwait(false);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task PublishRoute_ShouldCallRepository()
    {
        // Arrange
        var dto = new PublishedRouteDto();
        this._routeRepositoryMock.Setup(r => r.PublishRoute(dto)).ReturnsAsync(true);

        // Act
        var result = await this._routeService.PublishRoute(dto).ConfigureAwait(false);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetRoutesNearAsync_ShouldReturnRoutes()
    {
        // Arrange
        var expectedRoutes = new List<PublishedRouteDto> { new(), new() };
        this._routeRepositoryMock.Setup(r => r.GetRoutesNearAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                            .ReturnsAsync(expectedRoutes);

        // Act
        var result = await this._routeService.GetRoutesNearAsync(41.4, 2.16, 1000).ConfigureAwait(false);

        // Assert
        Assert.Equal(expectedRoutes.Count, result.Count);
    }
}