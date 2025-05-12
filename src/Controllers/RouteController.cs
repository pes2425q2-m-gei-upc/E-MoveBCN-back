using System;
using System.Threading.Tasks;
using AutoMapper;
using Dto;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interface;
using src.Dto.Route;

[ApiController]
[Route("api/[controller]")] // api/rutas
[Authorize]
public class RutasController : ControllerBase
{
  private readonly IRouteService _routeService;
  private readonly IRouteRepository _routeRepository;

  public RutasController(IRouteService routeService, IRouteRepository routeRepository)
  {
    _routeService = routeService;
    _routeRepository = routeRepository;
  }

  [HttpPost("calcular")] // api/rutas/calcular
  public async Task<IActionResult> CalcularRuta([FromBody] RouteRequestDto dto)
  {
    Guid usuarioId = Guid.Parse("11111111-1111-1111-1111-111111111111"); // to-do: poner usuario autenticado
    var resultado = await _routeService.CalcularRutaAsync(dto, usuarioId).ConfigureAwait(false);
    return Ok(resultado);
  }
  [HttpPost("save")] // api/rutas/save
  public async Task<IActionResult> SaveRoute([FromBody] RouteDto dto)
  {
    var result = await _routeService.SaveRoute(dto).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Error saving route");
    }
    return Ok("Route saved successfully");
  }
}
