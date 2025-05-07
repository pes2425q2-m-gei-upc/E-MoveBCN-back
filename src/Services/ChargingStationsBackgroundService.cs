using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Interface;

namespace Services;

public class ChargingStationsBackgroundService : BackgroundService
{
  private readonly IServiceProvider _serviceProvider;  // Cambiado para usar IServiceProvider
  private readonly ILogger<ChargingStationsBackgroundService> _logger;
  private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

  public ChargingStationsBackgroundService(
      IServiceProvider serviceProvider,  // Inyectamos IServiceProvider
      ILogger<ChargingStationsBackgroundService> logger)
  {
    _serviceProvider = serviceProvider;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        _logger.LogInformation("Starting periodic update of charging stations");

        // Crear un scope para obtener el servicio scoped
        using (var scope = _serviceProvider.CreateScope())
        {
          var chargingService = scope.ServiceProvider.GetRequiredService<IChargingStationsService>();

          await chargingService.FetchAndStoreChargingStationsAsync().ConfigureAwait(false);
        }

        _logger.LogInformation("Completed periodic update of charging stations");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error during periodic update of charging stations");
      }

      await Task.Delay(_interval, stoppingToken).ConfigureAwait(false);
    }
  }
}
