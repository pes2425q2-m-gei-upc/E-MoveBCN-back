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

  public RutasController(IRouteService routeService)
  {
    _routeService = routeService;
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
  [HttpDelete("delete")] // api/rutas/delete
  public async Task<IActionResult> DeleteRoute([FromBody] string routeId)
  {
    var result = await _routeService.DeleteRoute(routeId).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Error deleting route");
    }
    return Ok("Route deleted successfully");
  }
  [HttpPost("publish")] // api/rutas/publish
  public async Task<IActionResult> PublishRoute([FromBody] PublishedRouteDto publishedRouteDto)
  {
    var result = await _routeService.PublishRoute(publishedRouteDto).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Error publishing route");
    }
    return Ok("Route published successfully");
  }
  [HttpDelete("deletepublished")] // api/rutas/deletepublished
  public async Task<IActionResult> DeletePublishedRoute([FromBody] string routeId)
  {
    var result = await _routeService.DeletePublishedRoute(routeId).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Error deleting published route");
    }
    return Ok("Published route deleted successfully");
  }
  [HttpGet("getroutesnear")] // api/rutas/getroutesnear
  public async Task<IActionResult> GetRoutesNear([FromQuery] double lat, [FromQuery] double lon, [FromQuery] double radiusInMeters)
  {
    var result = await _routeService.GetRoutesNearAsync(lat, lon, radiusInMeters).ConfigureAwait(false);
    if (result == null)
    {
      return NotFound("No routes found");
    }
    return Ok(result);
  }
}
