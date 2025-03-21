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

        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDadesObertesRepository, DadesObertesRepository>();
    }
}