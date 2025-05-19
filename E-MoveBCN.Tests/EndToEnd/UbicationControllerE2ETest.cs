using System.Net.Http.Json;
using System.Text.Json;
using Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

public class UbicationControllerE2ETest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UbicationControllerE2ETest(WebApplicationFactory<Program> factory)
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
    public async Task SaveUbication_ShouldReturnOk()
    {
        var ubication = new SavedUbicationDto
        {
            UserEmail = "test@example.com",
            UbicationId = 1,
            Latitude = 41.40338,
            Longitude = 2.17403,
            StationType = "electric"
        };

        var response = await _client.PostAsJsonAsync("/api/ubication/save", ubication);

        response.EnsureSuccessStatusCode(); // 200-299
    }

    [Fact]
    public async Task GetUbicationsByEmail_ShouldReturnList()
    {
        var response = await _client.GetAsync("/api/ubication/test@example.com");

        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var list = JsonSerializer.Deserialize<List<SavedUbicationDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(list);
    }
}
