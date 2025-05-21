using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Interface;
namespace Services;
public class BicingStationBackgroundService(
    IServiceProvider serviceProvider,  // Inyectamos IServiceProvider
    ILogger<BicingStationBackgroundService> logger) : BackgroundService
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;  // Usamos IServiceProvider
  private readonly ILogger<BicingStationBackgroundService> _logger = logger;
  private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        const int maxWaitTimeSeconds = 30;
        const int checkIntervalMs = 1000;
        int elapsedSeconds = 0;

        // 1. Espera activa verificando conexiones activas
        while (elapsedSeconds < maxWaitTimeSeconds)
        {
          await Task.Delay(checkIntervalMs, stoppingToken).ConfigureAwait(false);
          elapsedSeconds++;
        }

        this._logger.LogInformation("Starting periodic update of bicing stations");

        // Crear un scope y obtener el servicio scoped dentro del scope
        using (var scope = this._serviceProvider.CreateScope())
        {
          var bicingService = scope.ServiceProvider.GetRequiredService<IBicingStationService>();
          await bicingService.FetchAndStoreBicingStationsAsync().ConfigureAwait(false);
        }

        this._logger.LogInformation("Completed periodic update of bicing stations");
      }
      catch (OperationCanceledException)
      {
        // Allow the task to be cancelled gracefully.
        throw;
      }
      catch (Exception ex)
      {
        this._logger.LogError(ex, "Error during periodic update of bicing stations");
        throw;
      }

      await Task.Delay(this._interval, stoppingToken).ConfigureAwait(false);
    }
  }
}
