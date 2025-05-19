using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Newtonsoft.Json.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Dto;
using System.Net.Http.Json;

public class TmbControllerE2ETests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TmbControllerE2ETests(WebApplicationFactory<Program> factory)
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
    public async Task GetAllMetros_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/api/tmb/metros");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrWhiteSpace();
        JToken.Parse(content).Type.Should().Be(JTokenType.Array);
    }

    [Fact]
    public async Task GetAllBus_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/api/tmb/bus");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrWhiteSpace();
        JToken.Parse(content).Type.Should().Be(JTokenType.Array);
    }
}
