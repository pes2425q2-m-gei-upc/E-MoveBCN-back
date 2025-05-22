using AutoMapper;
using Dto.User;
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

        this._mapper = AutoMapperFactory.CreateMapper();
        this._dbContext = new ApiDbContext(options, configuration);
        this._userRepository = new UserRepository(this._dbContext, this._mapper);

        this._testUser = new UserCreate
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
        var created = this._userRepository.CreateUser(this._testUser);
        var fetchedUser = await this._userRepository.GetUserByEmailAsync(this._testUser.Email).ConfigureAwait(false);

        // Assert
        Assert.True(created);
        Assert.NotNull(fetchedUser);
        Assert.Equal(this._testUser.Email, fetchedUser.Email);
    }

    [Fact]
    public async Task DeleteUser_ShouldRemoveFromDatabase()
    {
        // Arrange
        this._userRepository.CreateUser(this._testUser);
        var fetched = await this._userRepository.GetUserByEmailAsync(this._testUser.Email).ConfigureAwait(false);

        // Act
        Assert.NotNull(fetched); // Ensure fetched is not null before accessing UserId
        var deleted = await this._userRepository.DeleteUser(fetched.UserId).ConfigureAwait(false);

        // Assert
        Assert.True(deleted);
        var afterDelete = await this._userRepository.GetUserByEmailAsync(this._testUser.Email).ConfigureAwait(false);
        Assert.Null(afterDelete);
    }

    public async ValueTask DisposeAsync()
    {
        // Elimina ubicaciones guardadas antes de eliminar el usuario
        await this._dbContext.SavedUbications
            .Where(u => u.UserEmail == this._testUser.Email)
            .ExecuteDeleteAsync()
            .ConfigureAwait(false);

        var existingUser = await this._dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == this._testUser.Email)
            .ConfigureAwait(false);

        if (existingUser != null)
        {
            this._dbContext.Users.Remove(existingUser);
            await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}

