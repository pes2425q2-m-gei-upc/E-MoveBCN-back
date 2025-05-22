using AutoMapper;
using Entity;
using Microsoft.EntityFrameworkCore;
using Dto.Route;
using Entity.Route;
using TestUtils;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Repositories;
using Entity.User;

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

        this._mapper = AutoMapperFactory.CreateMapper();
        this._dbContext = new ApiDbContext(options, configuration);
        this._routeRepository = new RouteRepository(this._dbContext, this._mapper);
    }

    public async Task InitializeAsync()
    {
        this._userId = Guid.NewGuid();
        this._routeId = Guid.NewGuid();

        // Eliminar si existe previamente
        var existingUser = await this._dbContext.Users.FirstOrDefaultAsync(u => u.Email == this._userEmail).ConfigureAwait(false);
        if (existingUser != null)
        {
            this._dbContext.Users.Remove(existingUser);
            await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        this._dbContext.Users.Add(new UserEntity
        {
            UserId = this._userId,
            Email = this._userEmail,
            Username = "Test User",
            PasswordHash = "hashed"
        });

        await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    [Fact]
    public async Task SavePublishDeleteRoute_ShouldSucceed()
    {
        var route = new RouteEntity
        {
            RouteId = this._routeId,
            UserId = this._userId,
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

        var saveResult = await this._routeRepository.GuardarRutaAsync(route).ConfigureAwait(false);
        saveResult.Should().BeTrue();

        var publishDto = new PublishedRouteDto
        {
            RouteId = this._routeId.ToString(),
            Date = DateTime.UtcNow,
            AvailableSeats = 3
        };

        var publishResult = await this._routeRepository.PublishRoute(publishDto).ConfigureAwait(false);
        publishResult.Should().BeTrue();

        var deletePublishedResult = await this._routeRepository.DeletePublishedRoute(this._routeId.ToString()).ConfigureAwait(false);
        deletePublishedResult.Should().BeTrue();

        var deleteRouteResult = await this._routeRepository.DeleteRoute(this._routeId.ToString()).ConfigureAwait(false);
        deleteRouteResult.Should().BeTrue();
    }

    public async Task DisposeAsync()
    {
        // Limpieza completa para evitar residuos en DB
        this._dbContext.PublishedRoutes.RemoveRange(this._dbContext.PublishedRoutes);
        this._dbContext.Routes.RemoveRange(this._dbContext.Routes);
        await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}
