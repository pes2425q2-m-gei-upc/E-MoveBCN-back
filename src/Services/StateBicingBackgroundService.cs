using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Interface;
namespace Services;
public class StateBicingBackgroundService(
    IServiceProvider serviceProvider,  // Inyectamos IServiceProvider
    ILogger<StateBicingBackgroundService> logger) : BackgroundService
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;  // Usamos IServiceProvider
  private readonly ILogger<StateBicingBackgroundService> _logger = logger;
  private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        const int maxWaitTimeSeconds = 60;
        const int checkIntervalMs = 1000;
        int elapsedSeconds = 0;

        // 1. Espera activa verificando conexiones activas
        while (elapsedSeconds < maxWaitTimeSeconds)
        {
          await Task.Delay(checkIntervalMs, stoppingToken).ConfigureAwait(false);
          elapsedSeconds++;
        }

        this._logger.LogInformation("Starting periodic update of the state of bicing stations");

        // Crear un scope y obtener el servicio scoped dentro del scope
        using (var scope = this._serviceProvider.CreateScope())
        {
          var stateBicingService = scope.ServiceProvider.GetRequiredService<IStateBicingService>();
          await stateBicingService.FetchAndStoreStateBicingStationsAsync().ConfigureAwait(false);
        }

        this._logger.LogInformation("Completed periodic update of the state of bicing stations");
      }
      catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
      {
        // Graceful shutdown, no action needed.
      }
      catch (Exception ex)
      {
        this._logger.LogError(ex, "Error during periodic update of the state of bicing stations");
        throw; // Rethrow to avoid swallowing unexpected exceptions
      }

      await Task.Delay(this._interval, stoppingToken).ConfigureAwait(false);
    }
  }
}
