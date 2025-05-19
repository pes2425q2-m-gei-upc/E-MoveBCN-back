using Xunit;
using Moq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Services;
using System.Text;
using Moq.Protected;

public class TmbServiceTests
{
    private readonly Mock<IConfiguration> _configMock = new();
    private readonly Mock<HttpMessageHandler> _httpHandlerMock = new();
    private readonly HttpClient _httpClient;
    private readonly TmbService _tmbService;

    public TmbServiceTests()
    {
        _httpClient = new HttpClient(_httpHandlerMock.Object);

        _configMock.Setup(c => c["TmbApi:ApiKey"]).Returns("dummy-api-key");
        _configMock.Setup(c => c["TmbApi:AppId"]).Returns("dummy-app-id");
        _configMock.Setup(c => c["TmbApi:BaseUrl"]).Returns("https://dummy.api/");
        _configMock.Setup(c => c["TmbApi:Metros"]).Returns("metros");
        _configMock.Setup(c => c["TmbApi:Bus"]).Returns("bus");

        _tmbService = new TmbService(_httpClient, _configMock.Object);
    }

    [Fact]
    public async Task GetAllMetrosAsync_ShouldReturnMetroList_WhenApiResponseIsValid()
    {
        // Arrange
        var jsonResponse = @"
        {
            ""features"": [
                {
                    ""properties"": { ""ID_ESTACIO"": 1, ""NOM_ESTACIO"": ""Estació Test"" },
                    ""geometry"": { ""coordinates"": [2.17, 41.38] }
                }
            ]
        }";

        SetupHttpResponse("https://dummy.api/metros?app_id=dummy-app-id&app_key=dummy-api-key", jsonResponse);

        // Act
        var result = await _tmbService.GetAllMetrosAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result[0].IdEstacio);
        Assert.Equal("Estació Test", result[0].NomEstacio);
    }

    [Fact]
    public async Task GetAllBusAsync_ShouldReturnBusList_WhenApiResponseIsValid()
    {
        // Arrange
        var jsonResponse = @"
        {
            ""features"": [
                {
                    ""properties"": { ""ID_PARADA"": 123, ""NOM_PARADA"": ""Parada Test"" },
                    ""geometry"": { ""coordinates"": [2.18, 41.39] }
                }
            ]
        }";

        SetupHttpResponse("https://dummy.api/bus?app_id=dummy-app-id&app_key=dummy-api-key", jsonResponse);

        // Act
        var result = await _tmbService.GetAllBusAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal(123, result[0].ParadaId);
        Assert.Equal("Parada Test", result[0].Name);
    }

    [Fact]
    public async Task GetMetroByIdAsync_ShouldReturnCorrectMetro()
    {
        // Arrange
        var jsonResponse = @"
        {
            ""features"": [
                {
                    ""properties"": { ""ID_ESTACIO"": 10, ""NOM_ESTACIO"": ""Estació X"" },
                    ""geometry"": { ""coordinates"": [2.0, 41.0] }
                },
                {
                    ""properties"": { ""ID_ESTACIO"": 20, ""NOM_ESTACIO"": ""Estació Y"" },
                    ""geometry"": { ""coordinates"": [2.1, 41.1] }
                }
            ]
        }";

        SetupHttpResponse("https://dummy.api/metros?app_id=dummy-app-id&app_key=dummy-api-key", jsonResponse);

        // Act
        var result = await _tmbService.GetMetroByIdAsync(20);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(20, result.IdEstacio);
        Assert.Equal("Estació Y", result.NomEstacio);
    }

    [Fact]
    public async Task GetBusByIdAsync_ShouldReturnCorrectBus()
    {
        // Arrange
        var jsonResponse = @"
        {
            ""features"": [
                {
                    ""properties"": { ""ID_PARADA"": 1, ""NOM_PARADA"": ""Parada A"" },
                    ""geometry"": { ""coordinates"": [2.0, 41.0] }
                },
                {
                    ""properties"": { ""ID_PARADA"": 2, ""NOM_PARADA"": ""Parada B"" },
                    ""geometry"": { ""coordinates"": [2.1, 41.1] }
                }
            ]
        }";

        SetupHttpResponse("https://dummy.api/bus?app_id=dummy-app-id&app_key=dummy-api-key", jsonResponse);

        // Act
        var result = await _tmbService.GetBusByIdAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.ParadaId);
        Assert.Equal("Parada B", result.Name);
    }

    private void SetupHttpResponse(string expectedUrl, string content)
{
    _httpHandlerMock
        .Protected()
        .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Get &&
                req.RequestUri.ToString() == expectedUrl),
            ItExpr.IsAny<CancellationToken>())
        .ReturnsAsync(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        });
}
}
