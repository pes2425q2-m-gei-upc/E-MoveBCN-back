using Xunit;
using Moq;
using Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using FluentAssertions;
using TestUtils;
using Services;
using Dto.User;
namespace E_MoveBCN.Tests.Unit;
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        this._userRepositoryMock = new Mock<IUserRepository>();
        this._userService = new UserService(this._userRepositoryMock.Object);
    }

    [Fact]
    public void GetAllUsers_ShouldReturnAllUsers()
    {
        var expectedUsers = new List<UserDto>
        {
            TestUserFactory.CreateValidUserDto(),
            TestUserFactory.CreateValidUserDto()
        };

        this._userRepositoryMock.Setup(repo => repo.GetAllUsers()).Returns(expectedUsers);

        var result = this._userService.GetAllUsers();

        result.Should().BeEquivalentTo(expectedUsers);
    }

    [Fact]
    public void CreateUser_ShouldReturnTrue_WhenRepositoryReturnsTrue()
    {
        var user = TestUserFactory.CreateValidUserCreate();
        this._userRepositoryMock.Setup(repo => repo.CreateUser(user)).Returns(true);

        var result = this._userService.CreateUser(user);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Authenticate_ShouldReturnUser_WhenCredentialsAreValid()
    {
        var credentials = TestUserFactory.CreateUserCredentials();
        var passwordHash = new PasswordHasher<string>().HashPassword(null, credentials.Password);
        var userFromDb = TestUserFactory.CreateValidUserDto(email: credentials.UserEmail, passwordHash: passwordHash);

        this._userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(credentials.UserEmail)).ReturnsAsync(userFromDb);

        var result = await this._userService.Authenticate(credentials).ConfigureAwait(false);

        result.Should().NotBeNull();
        result.Email.Should().Be(credentials.UserEmail);
    }

    [Fact]
    public async Task Authenticate_ShouldReturnNull_WhenUserNotFound()
    {
        var credentials = TestUserFactory.CreateUserCredentials("notfound@example.com", "password");
        this._userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(credentials.UserEmail)).ReturnsAsync((UserDto?)null);

        var result = await this._userService.Authenticate(credentials).ConfigureAwait(false);

        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnFalse_WhenPasswordVerificationFails()
    {
        var credentials = TestUserFactory.CreateUserCredentials("fail@example.com", "wrong");
        var user = TestUserFactory.CreateValidUserDto(
            email: "fail@example.com",
            passwordHash: new PasswordHasher<string>().HashPassword(string.Empty, "correct")
        );

        this._userRepositoryMock.Setup(r => r.GetUserByEmailAsync(credentials.UserEmail)).ReturnsAsync(user);

        var result = await this._userService.DeleteUser(credentials).ConfigureAwait(false);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnTrue_WhenUserIsValid()
    {
        var credentials = TestUserFactory.CreateUserCredentials("success@example.com", "pass");
        var hash = new PasswordHasher<string>().HashPassword(string.Empty, "pass");
        var user = TestUserFactory.CreateValidUserDto(email: credentials.UserEmail, passwordHash: hash);

        this._userRepositoryMock.Setup(r => r.GetUserByEmailAsync(credentials.UserEmail)).ReturnsAsync(user);
        this._userRepositoryMock.Setup(r => r.DeleteUser(It.IsAny<string>())).ReturnsAsync(true);

        var result = await this._userService.DeleteUser(credentials).ConfigureAwait(false);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task ModifyUser_ShouldCallRepositoryAndReturnResult()
    {
        var userDto = TestUserFactory.CreateValidUserDto();
        this._userRepositoryMock.Setup(r => r.ModifyUser(userDto)).ReturnsAsync(true);

        var result = await this._userService.ModifyUser(userDto).ConfigureAwait(false);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task LoginWithGoogleAsync_ShouldReturnExistingUser_IfExists()
    {
        var dto = TestUserFactory.CreateGoogleDto("exists@example.com", "existinguser");
        var existingUser = TestUserFactory.CreateValidUserDto(email: dto.Email);

        this._userRepositoryMock.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync(existingUser);

        var result = await this._userService.LoginWithGoogleAsync(dto).ConfigureAwait(false);

        result.Should().BeEquivalentTo(existingUser);
    }

    [Fact]
    public async Task LoginWithGoogleAsync_ShouldCreateUserAndReturnIt()
    {
        var dto = TestUserFactory.CreateGoogleDto("new@example.com", "newuser");

        this._userRepositoryMock.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync((UserDto?)null);
        this._userRepositoryMock.Setup(r => r.CreateGoogleUserAsync(dto.Username, dto.Email)).ReturnsAsync(true);
        this._userRepositoryMock.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync(
            TestUserFactory.CreateValidUserDto(email: dto.Email)
        );

        var result = await this._userService.LoginWithGoogleAsync(dto).ConfigureAwait(false);

        result.Email.Should().Be(dto.Email);
    }

    [Fact]
    public async Task LoginWithGoogleAsync_ShouldThrowException_IfUserCannotBeCreated()
    {
        var dto = TestUserFactory.CreateGoogleDto("error@example.com", "user");

        this._userRepositoryMock.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync((UserDto?)null);
        this._userRepositoryMock.Setup(r => r.CreateGoogleUserAsync(dto.Username, dto.Email)).ReturnsAsync(false);

        await Assert.ThrowsAsync<Exception>(() => this._userService.LoginWithGoogleAsync(dto)).ConfigureAwait(false);
    }
}
