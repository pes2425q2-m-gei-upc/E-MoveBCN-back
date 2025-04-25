using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Interface;

namespace Services
{
    public class StateBicingBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;  // Usamos IServiceProvider
        private readonly ILogger<StateBicingBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

        public StateBicingBackgroundService(
            IServiceProvider serviceProvider,  // Inyectamos IServiceProvider
            ILogger<StateBicingBackgroundService> logger)
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
                    const int maxWaitTimeSeconds = 60;
                    const int checkIntervalMs = 1000;
                    int elapsedSeconds = 0;

                    // 1. Espera activa verificando conexiones activas
                    while (elapsedSeconds < maxWaitTimeSeconds)
                    {
                        await Task.Delay(checkIntervalMs);
                        elapsedSeconds++;
                    }

                    _logger.LogInformation("Starting periodic update of the state of bicing stations");

                    // Crear un scope y obtener el servicio scoped dentro del scope
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var stateBicingService = scope.ServiceProvider.GetRequiredService<IStateBicingService>();
                        await stateBicingService.FetchAndStoreStateBicingStationsAsync();
                    }

                    _logger.LogInformation("Completed periodic update of the state of bicing stations");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during periodic update of the state of bicing stations");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
