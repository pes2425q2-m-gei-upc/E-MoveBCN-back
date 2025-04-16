using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services;
using System;
using Entity;
using Dto;
using AutoMapper;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Services.Interface;

[ApiController]
[Route("api/[controller]")] // api/rutas
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
        Guid usuarioId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var resultado = await _service.CalcularRutaAsync(dto, usuarioId);
        return Ok(resultado);
    }
}