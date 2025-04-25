using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Interface;

namespace Services
{
    public class BicingStationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;  // Usamos IServiceProvider
        private readonly ILogger<BicingStationBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

        public BicingStationBackgroundService(
            IServiceProvider serviceProvider,  // Inyectamos IServiceProvider
            ILogger<BicingStationBackgroundService> logger)
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
                    const int maxWaitTimeSeconds = 30;
                    const int checkIntervalMs = 1000;
                    int elapsedSeconds = 0;

                    // 1. Espera activa verificando conexiones activas
                    while (elapsedSeconds < maxWaitTimeSeconds)
                    {
                        await Task.Delay(checkIntervalMs);
                        elapsedSeconds++;
                    }

                    _logger.LogInformation("Starting periodic update of bicing stations");

                    // Crear un scope y obtener el servicio scoped dentro del scope
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var bicingService = scope.ServiceProvider.GetRequiredService<IBicingStationService>();
                        await bicingService.FetchAndStoreBicingStationsAsync();
                    }

                    _logger.LogInformation("Completed periodic update of bicing stations");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during periodic update of bicing stations");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
