using System.Net;
using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Dto;
using Microsoft.VisualStudio.TestPlatform.TestHost;

public class UserControllerE2ETest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserControllerE2ETest(WebApplicationFactory<Program> factory)
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
    public async Task CreateUser_And_GetUsers_ShouldSucceed()
    {
        // Arrange
        var newUser = new UserCreate
        {
            Username = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "Test1234"
        };

        // Act - Create user
        var createResponse = await _client.PostAsJsonAsync("/api/user/createuser", newUser);
        var createContent = await createResponse.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
        Assert.Contains("User created successfully", createContent);

        // Act - Get users
        var getResponse = await _client.GetAsync("/api/user/getusers");
        var getUsers = await getResponse.Content.ReadFromJsonAsync<List<UserDto>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Contains(getUsers, u => u.Email == "testuser@example.com");
    }
}
