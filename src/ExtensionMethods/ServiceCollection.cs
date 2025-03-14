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

        //Services
        services.AddScoped<IUserService, UserService>();

        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();

    }
}