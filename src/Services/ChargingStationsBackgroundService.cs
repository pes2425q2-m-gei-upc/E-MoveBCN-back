using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Interface;

namespace Services
{
    public class ChargingStationsBackgroundService : BackgroundService
    {
        private readonly IChargingStationsService _service;
        private readonly ILogger<ChargingStationsBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

        public ChargingStationsBackgroundService(
            IChargingStationsService service,
            ILogger<ChargingStationsBackgroundService> logger)
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
                    _logger.LogInformation("Starting periodic update of charging stations");
                    await _service.FetchAndStoreChargingStationsAsync();
                    _logger.LogInformation("Completed periodic update of charging stations");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during periodic update of charging stations");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}