using System;
using System.Threading.Tasks;
using AutoMapper;
using Dto;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interface;

[ApiController]
[Route("api/[controller]")] // api/rutas
[Authorize]
public class RutasController : ControllerBase
{
  private readonly IRouteService _service;
  private readonly IRouteRepository _repo;

  public RutasController(IRouteService service, IRouteRepository repo)
  {
    _service = service;
    _repo = repo;
  }

  [HttpPost("calcular")]
  public async Task<IActionResult> CalcularRuta([FromBody] RouteRequestDto dto)
  {
    Guid usuarioId = Guid.Parse("11111111-1111-1111-1111-111111111111"); // to-do: poner usuario autenticado
    var resultado = await _service.CalcularRutaAsync(dto, usuarioId).ConfigureAwait(false);
    return Ok(resultado);
  }
}
