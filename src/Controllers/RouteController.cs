using System.Threading.Tasks;
using Dto.Route;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
namespace Controllers;
[ApiController]
[Route("api/[controller]")] // api/rutas
[Authorize]
public class RutasController(IRouteService routeService) : ControllerBase
{
  private readonly IRouteService _routeService = routeService;

  [HttpPost("calcular")] // api/rutas/calcular
  public async Task<IActionResult> CalcularRuta([FromBody] RouteRequestDto route)
  {
    var result = await this._routeService.CalcularRutaAsync(route).ConfigureAwait(false);
    return Ok(result);
  }
  [HttpPost("save")] // api/rutas/save
  public async Task<IActionResult> SaveRoute([FromBody] RouteDto dto)
  {
    var result = await this._routeService.SaveRoute(dto).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Error saving route");
    }
    return Ok("Route saved successfully");
  }
  [HttpDelete("delete")] // api/rutas/delete
  public async Task<IActionResult> DeleteRoute([FromBody] string routeId)
  {
    var result = await this._routeService.DeleteRoute(routeId).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Error deleting route");
    }
    return Ok("Route deleted successfully");
  }
  [HttpPost("publish")] // api/rutas/publish
  public async Task<IActionResult> PublishRoute([FromBody] PublishedRouteDto publishedRouteDto)
  {
    var result = await this._routeService.PublishRoute(publishedRouteDto).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Error publishing route");
    }
    return Ok("Route published successfully");
  }
  [HttpDelete("deletepublished")] // api/rutas/deletepublished
  public async Task<IActionResult> DeletePublishedRoute([FromBody] string routeId)
  {
    var result = await this._routeService.DeletePublishedRoute(routeId).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Error deleting published route");
    }
    return Ok("Published route deleted successfully");
  }
  [HttpGet("getroutesnear")] // api/rutas/getroutesnear
  public async Task<IActionResult> GetRoutesNear([FromQuery] double lat, [FromQuery] double lon, [FromQuery] double radiusInMeters)
  {
    var result = await this._routeService.GetRoutesNearAsync(lat, lon, radiusInMeters).ConfigureAwait(false);
    if (result == null)
    {
      return NotFound("No routes found");
    }
    return Ok(result);
  }
  [HttpGet("savedroute")] // api/rutas/savedroute
  public async Task<IActionResult> GetSavedRoute([FromQuery] string userId)
  {
    var result = await this._routeService.GetSavedRoute(userId).ConfigureAwait(false);
    if (result == null)
    {
      return NotFound("No routes found");
    }
    return Ok(result);
  }
  [HttpGet("streetname")] // api/rutas/streetname
  public async Task<IActionResult> GetStreetName([FromQuery] double lat, [FromQuery] double lon)
  {
    var result = await this._routeService.GetStreetNameAsync(lat, lon).ConfigureAwait(false);
    if (result == null)
    {
      return NotFound("No street found");
    }
    return Ok(result);
  }
}
