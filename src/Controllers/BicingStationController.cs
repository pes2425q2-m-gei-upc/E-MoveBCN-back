using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Dto;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class BicingStationController : ControllerBase
{
    private readonly IBicingStationService _bicingStationService;
    private readonly ILogger<BicingStationController> _logger;

    public BicingStationController(IBicingStationService bicingStationService, ILogger<BicingStationController> logger)
    {
        _bicingStationService = bicingStationService;
        _logger = logger;
    }

    // GET: api/BicingStation
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var stations = await _bicingStationService.GetAllBicingStationsAsync();
            return Ok(stations);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error getting all bicing stations: {ex.Message}");
            return StatusCode(500, "Error interno del servidor al obtener las estaciones de bicing");
        }
    }

    [HttpPost("fetch-and-store")]
    public async Task<IActionResult> FetchAndStoreChargingStations()
    {
        await _bicingStationService.FetchAndStoreBicingStationsAsync();
        return Ok("Charging stations fetched and stored successfully.");
    }
} 