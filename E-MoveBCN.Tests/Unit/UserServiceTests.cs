using Xunit;
using Moq;
using Repositories.Interface;
using Dto;
using Microsoft.AspNetCore.Identity;
using FluentAssertions;
using src.Services;
using TestUtils;

namespace Unit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void GetAllUsers_ShouldReturnAllUsers()
    {
        var expectedUsers = new List<UserDto>
        {
            TestUserFactory.CreateValidUserDto(),
            TestUserFactory.CreateValidUserDto()
        };

        _userRepositoryMock.Setup(repo => repo.GetAllUsers()).Returns(expectedUsers);

        var result = _userService.GetAllUsers();

        result.Should().BeEquivalentTo(expectedUsers);
    }

    [Fact]
    public void CreateUser_ShouldReturnTrue_WhenRepositoryReturnsTrue()
    {
        var user = TestUserFactory.CreateValidUserCreate();
        _userRepositoryMock.Setup(repo => repo.CreateUser(user)).Returns(true);

        var result = _userService.CreateUser(user);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Authenticate_ShouldReturnUser_WhenCredentialsAreValid()
    {
        var credentials = TestUserFactory.CreateUserCredentials();
        var passwordHash = new PasswordHasher<string>().HashPassword(null, credentials.Password);
        var userFromDb = TestUserFactory.CreateValidUserDto(email: credentials.UserEmail, passwordHash: passwordHash);

        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(credentials.UserEmail)).ReturnsAsync(userFromDb);

        var result = await _userService.Authenticate(credentials);

        result.Should().NotBeNull();
        result.Email.Should().Be(credentials.UserEmail);
    }

    [Fact]
    public async Task Authenticate_ShouldReturnNull_WhenUserNotFound()
    {
        var credentials = TestUserFactory.CreateUserCredentials("notfound@example.com", "password");
        _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(credentials.UserEmail)).ReturnsAsync((UserDto)null);

        var result = await _userService.Authenticate(credentials);

        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnFalse_WhenPasswordVerificationFails()
    {
        var credentials = TestUserFactory.CreateUserCredentials("fail@example.com", "wrong");
        var user = TestUserFactory.CreateValidUserDto(
            email: "fail@example.com",
            passwordHash: new PasswordHasher<string>().HashPassword(null, "correct")
        );

        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(credentials.UserEmail)).ReturnsAsync(user);

        var result = await _userService.DeleteUser(credentials);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnTrue_WhenUserIsValid()
    {
        var credentials = TestUserFactory.CreateUserCredentials("success@example.com", "pass");
        var hash = new PasswordHasher<string>().HashPassword(null, "pass");
        var user = TestUserFactory.CreateValidUserDto(email: credentials.UserEmail, passwordHash: hash);

        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(credentials.UserEmail)).ReturnsAsync(user);
        _userRepositoryMock.Setup(r => r.DeleteUser(It.IsAny<string>())).ReturnsAsync(true);

        var result = await _userService.DeleteUser(credentials);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task ModifyUser_ShouldCallRepositoryAndReturnResult()
    {
        var userDto = TestUserFactory.CreateValidUserDto();
        _userRepositoryMock.Setup(r => r.ModifyUser(userDto)).ReturnsAsync(true);

        var result = await _userService.ModifyUser(userDto);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task LoginWithGoogleAsync_ShouldReturnExistingUser_IfExists()
    {
        var dto = TestUserFactory.CreateGoogleDto("exists@example.com", "existinguser");
        var existingUser = TestUserFactory.CreateValidUserDto(email: dto.Email);

        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync(existingUser);

        var result = await _userService.LoginWithGoogleAsync(dto);

        result.Should().BeEquivalentTo(existingUser);
    }

    [Fact]
    public async Task LoginWithGoogleAsync_ShouldCreateUserAndReturnIt()
    {
        var dto = TestUserFactory.CreateGoogleDto("new@example.com", "newuser");

        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync((UserDto)null);
        _userRepositoryMock.Setup(r => r.CreateGoogleUserAsync(dto.Username, dto.Email)).ReturnsAsync(true);
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync(
            TestUserFactory.CreateValidUserDto(email: dto.Email)
        );

        var result = await _userService.LoginWithGoogleAsync(dto);

        result.Email.Should().Be(dto.Email);
    }

    [Fact]
    public async Task LoginWithGoogleAsync_ShouldThrowException_IfUserCannotBeCreated()
    {
        var dto = TestUserFactory.CreateGoogleDto("error@example.com", "user");

        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(dto.Email)).ReturnsAsync((UserDto)null);
        _userRepositoryMock.Setup(r => r.CreateGoogleUserAsync(dto.Username, dto.Email)).ReturnsAsync(false);

        await Assert.ThrowsAsync<Exception>(() => _userService.LoginWithGoogleAsync(dto));
    }
}
