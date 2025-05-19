using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using src.Dto.Route;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Dto; // Asegúrate de usar los DTOs correctos

public class RouteControllerE2ETests : IClassFixture<WebApplicationFactory<Program>> // Cambia Program si usas otro EntryPoint
{
    private readonly HttpClient _client;

    public RouteControllerE2ETests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    UserCredentials login = new UserCredentials
    {
      UserEmail = "daniel.castellano.cd8@gmail.com",
      Password = "12345"
    };
    var loginResponse = _client.PostAsJsonAsync("/api/authorization/login", login);
    }

    [Fact]
    public async Task GetRoutesNear_ShouldReturnOk()
    {
        // Arrange
        var lat = 41.3869;
        var lon = 2.1701;
        var radius = 1000;

        var url = $"/api/rutas/getroutesnear?lat={lat}&lon={lon}&radiusInMeters={radius}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var data = await response.Content.ReadFromJsonAsync<List<PublishedRouteDto>>();
        Assert.NotNull(data);
    }
}
