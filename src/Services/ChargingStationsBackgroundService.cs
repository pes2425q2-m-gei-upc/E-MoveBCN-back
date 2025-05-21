using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Interface;
namespace Services;
public class ChargingStationsBackgroundService(
    IServiceProvider serviceProvider,  // Inyectamos IServiceProvider
    ILogger<ChargingStationsBackgroundService> logger) : BackgroundService
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;  // Cambiado para usar IServiceProvider
  private readonly ILogger<ChargingStationsBackgroundService> _logger = logger;
  private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        this._logger.LogInformation("Starting periodic update of charging stations");

        // Crear un scope para obtener el servicio scoped
        using (var scope = this._serviceProvider.CreateScope())
        {
          var chargingService = scope.ServiceProvider.GetRequiredService<IChargingStationsService>();

          await chargingService.FetchAndStoreChargingStationsAsync().ConfigureAwait(false);
        }

        this._logger.LogInformation("Completed periodic update of charging stations");
      }
      catch (InvalidOperationException ex)
      {
        this._logger.LogError(ex, "Invalid operation during periodic update of charging stations");
        throw;
      }
      catch (Exception ex)
      {
        this._logger.LogError(ex, "Unexpected error during periodic update of charging stations");
        throw;
      }

      await Task.Delay(this._interval, stoppingToken).ConfigureAwait(false);
    }
  }
}
