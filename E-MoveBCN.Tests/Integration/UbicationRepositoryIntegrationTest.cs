/*using AutoMapper;
using Dto;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using TestUtils;
using Xunit;
using FluentAssertions;
using Repositories.Interface;
[Collection("Sequential")]
public class UbicationRepositoryIntegrationTest : IAsyncLifetime
{
    private readonly ApiDbContext _dbContext;
    private readonly IUbicationRepository _ubicationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    private string _testUserEmail = "test@integration.com";

    public UbicationRepositoryIntegrationTest()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .Options;

        _mapper = AutoMapperFactory.CreateMapper();
        _dbContext = new ApiDbContext(options, configuration);
        _ubicationRepository = new UbicationRepository(_dbContext, _mapper);
        _userRepository = new UserRepository(_dbContext, _mapper);
    }

    [Fact]
    public async Task SaveAndRetrieveAndUpdateAndDeleteUbication_ShouldWorkCorrectly()
    {
        // Arrange: create a dummy user
        await _userRepository.CreateGoogleUserAsync("Test", _testUserEmail).ConfigureAwait(false);

        var savedUbication = new SavedUbicationDto
        {
            UserEmail = _testUserEmail,
            UbicationId = 999999,
            Latitude = 41.38879,
            Longitude = 2.15899,
            StationType = "BICING"
        };

        var ubicationInfo = new UbicationInfoDto
        {
            UserEmail = _testUserEmail,
            UbicationId = 999999,
            StationType = "BICING",
            Valoration = 4,
            Comment = "Updated comment"
        };

        // Act: create
        var saveResult = await _ubicationRepository.SaveUbicationAsync(savedUbication).ConfigureAwait(false);
        saveResult.Should().BeTrue();

        // Act: get
        var result = await _ubicationRepository.GetUbicationsByEmailAsync(_testUserEmail).ConfigureAwait(false);
        result.Should().ContainSingle(u => u.UbicationId == savedUbication.UbicationId);

        // Act: update
        var updateResult = await _ubicationRepository.UpdateUbication(ubicationInfo).ConfigureAwait(false);
        updateResult.Should().BeTrue();

        // Act: delete
        var deleteResult = await _ubicationRepository.DeleteUbication(ubicationInfo).ConfigureAwait(false);
        deleteResult.Should().BeTrue();
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync().ConfigureAwait(false);
    }

    public async Task DisposeAsync()
{
    await _dbContext.SavedUbications.ExecuteDeleteAsync();
    await _dbContext.Users.ExecuteDeleteAsync();
}
}*/
