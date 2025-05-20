using AutoMapper;
using Entity;
using Microsoft.EntityFrameworkCore;
using src.Dto.Route;
using src.Entity.Route;
using TestUtils;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

public class RouteRepositoryIntegrationTest : IAsyncLifetime
{
    private readonly ApiDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly RouteRepository _routeRepository;

    private readonly string _userEmail = $"testuser_{Guid.NewGuid()}@example.com";
    private Guid _userId;
    private Guid _routeId;

    public RouteRepositoryIntegrationTest()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .Options;

        _mapper = AutoMapperFactory.CreateMapper();
        _dbContext = new ApiDbContext(options, configuration);
        _routeRepository = new RouteRepository(_dbContext, _mapper);
    }

    public async Task InitializeAsync()
    {
        _userId = Guid.NewGuid();
        _routeId = Guid.NewGuid();

        // Eliminar si existe previamente
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == _userEmail);
        if (existingUser != null)
        {
            _dbContext.Users.Remove(existingUser);
            await _dbContext.SaveChangesAsync();
        }

        _dbContext.Users.Add(new UserEntity
        {
            UserId = _userId,
            Email = _userEmail,
            Username = "Test User",
            PasswordHash = "hashed"
        });

        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task SavePublishDeleteRoute_ShouldSucceed()
    {
        var route = new RouteEntity
        {
            RouteId = _routeId,
            UserId = _userId,
            OriginLat = 41.38f,
            OriginLng = 2.17f,
            DestinationLat = 41.39f,
            DestinationLng = 2.18f,
            Distance = 1000,
            Duration = 600,
            GeometryJson = "{}",
            InstructionsJson = "[]",
            Mean = "bike",
            Preference = "shortest",
            OriginStreetName = "Origin",
            DestinationStreetName = "Destination"
        };

        var saveResult = await _routeRepository.GuardarRutaAsync(route);
        saveResult.Should().BeTrue();

        var publishDto = new PublishedRouteDto
        {
            RouteId = _routeId.ToString(),
            Date = DateTime.UtcNow,
            AvailableSeats = 3
        };

        var publishResult = await _routeRepository.PublishRoute(publishDto);
        publishResult.Should().BeTrue();

        var deletePublishedResult = await _routeRepository.DeletePublishedRoute(_routeId.ToString());
        deletePublishedResult.Should().BeTrue();

        var deleteRouteResult = await _routeRepository.DeleteRoute(_routeId.ToString());
        deleteRouteResult.Should().BeTrue();
    }

    public async Task DisposeAsync()
    {
        // Limpieza completa para evitar residuos en DB
        _dbContext.PublishedRoutes.RemoveRange(_dbContext.PublishedRoutes);
        _dbContext.Routes.RemoveRange(_dbContext.Routes);
        await _dbContext.SaveChangesAsync();
    }
}
