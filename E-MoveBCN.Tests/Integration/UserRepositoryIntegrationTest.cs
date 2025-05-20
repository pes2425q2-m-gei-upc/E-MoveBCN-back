using AutoMapper;
using Dto;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.Interface;
using TestUtils;
using Xunit;

public class UserRepositoryIntegrationTest : IAsyncDisposable
{
    private readonly ApiDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserCreate _testUser;

    public UserRepositoryIntegrationTest()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .Options;

        _mapper = AutoMapperFactory.CreateMapper();
        _dbContext = new ApiDbContext(options, configuration);
        _userRepository = new UserRepository(_dbContext, _mapper);

        _testUser = new UserCreate
        {
            Username = "integration_test_user",
            Email = "integration_test@example.com",
            PasswordHash = "testpassword"
        };
    }

    [Fact]
    public async Task CreateUser_ThenGetUserByEmail_ShouldReturnSameUser()
    {
        // Act
        var created = _userRepository.CreateUser(_testUser);
        var fetchedUser = await _userRepository.GetUserByEmailAsync(_testUser.Email);

        // Assert
        Assert.True(created);
        Assert.NotNull(fetchedUser);
        Assert.Equal(_testUser.Email, fetchedUser.Email);
    }

    [Fact]
    public async Task DeleteUser_ShouldRemoveFromDatabase()
    {
        // Arrange
        _userRepository.CreateUser(_testUser);
        var fetched = await _userRepository.GetUserByEmailAsync(_testUser.Email);

        // Act
        var deleted = await _userRepository.DeleteUser(fetched.UserId);

        // Assert
        Assert.True(deleted);
        var afterDelete = await _userRepository.GetUserByEmailAsync(_testUser.Email);
        Assert.Null(afterDelete);
    }

    public async ValueTask DisposeAsync()
    {
        // Elimina ubicaciones guardadas antes de eliminar el usuario
        await _dbContext.SavedUbications
            .Where(u => u.UserEmail == _testUser.Email)
            .ExecuteDeleteAsync()
            .ConfigureAwait(false);

        var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == _testUser.Email)
            .ConfigureAwait(false);

        if (existingUser != null)
        {
            _dbContext.Users.Remove(existingUser);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}

