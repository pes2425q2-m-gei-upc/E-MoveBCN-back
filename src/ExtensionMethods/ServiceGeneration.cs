﻿using Helpers;
using Helpers.Interface;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Interface;
using Services;
using Services.Interface;
namespace ExtensionMethods;
public static class ServiceGeneration
{
  public static void AddServices(this IServiceCollection services)
  {
    //Helper
    services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
    services.AddScoped<IAireLliureHelper, AireLliureHelper>();

    //Services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IChargingStationsService, ChargingStationsService>();
    services.AddScoped<IStateBicingService, StateBicingService>();
    services.AddScoped<IBicingStationService, BicingStationService>();
    services.AddScoped<ITmbService, TmbService>();
    services.AddScoped<IRouteService, RouteService>();
    services.AddScoped<IUbicationService, UbicationService>();
    services.AddScoped<IChatService, ChatService>();
    services.AddScoped<IMessageService, MessageService>();
    services.AddHostedService<ChargingStationsBackgroundService>();
    services.AddHostedService<BicingStationBackgroundService>();
    services.AddHostedService<StateBicingBackgroundService>();

    //Repositories
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IChargingStationsRepository, ChargingStationsRepository>();
    services.AddScoped<IBicingStationRepository, BicingStationRepository>();
    services.AddScoped<IStateBicingRepository, StateBicingRepository>();
    services.AddScoped<IRouteRepository, RouteRepository>();
    services.AddScoped<IUbicationRepository, UbicationRepository>();
    services.AddScoped<IChatRepository, ChatRepository>();
    services.AddScoped<IMessageRepository, MessageRepository>();
  }
}
