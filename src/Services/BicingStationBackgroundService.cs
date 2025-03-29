using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Interface;

namespace Services
{
    public class BicingStationBackgroundService : BackgroundService
    {
        private readonly IBicingStationService _service;
        private readonly ILogger<BicingStationBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

        public BicingStationBackgroundService(
            IBicingStationService service,
            ILogger<BicingStationBackgroundService> logger)
        {
            _service = service;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    const int maxWaitTimeSeconds = 15;
                    const int checkIntervalMs = 1000;
                    int elapsedSeconds = 0;

                    // 1. Espera activa verificando conexiones activas
                    while (elapsedSeconds < maxWaitTimeSeconds)
                    {


                        await Task.Delay(checkIntervalMs);
                        elapsedSeconds++;
                    }

                    _logger.LogInformation("Starting periodic update of bicing stations");
                    await _service.FetchAndStoreBicingStationsAsync();
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