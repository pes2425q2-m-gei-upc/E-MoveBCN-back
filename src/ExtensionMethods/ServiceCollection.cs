using Helpers;
using Helpers.Interface;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Interface;
using Services;
using Services.Interface;


namespace ExtensionMethods;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services) {
        //Helper
        services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
        
        //Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDadesObertesService, DadesObertesService>();
        services.AddScoped<IStateBicingService, StateBicingService>();
        services.AddScoped<IBicingStationService, BicingStationService>();
        services.AddHostedService<ChargingStationsBackgroundService>();
        services.AddHostedService<BicingStationBackgroundService>();
        services.AddHostedService<StateBicingBackgroundService>();
        
        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDadesObertesRepository, DadesObertesRepository>();
         services.AddScoped<IBicingStationRepository, BicingStationRepository>();
        services.AddScoped<IStateBicingRepository, StateBicingRepository>();
    }
}