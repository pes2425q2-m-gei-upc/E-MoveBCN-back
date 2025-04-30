using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dto;
using Microsoft.Extensions.Configuration;
using Services.Interface;
using System;

[ApiController]
[Route("api/public")]
public class PublicRutasController : ControllerBase
{
    private readonly IRouteService _service;
    private readonly IConfiguration _config;

    public PublicRutasController(IRouteService service, IConfiguration config)
    {
        _service = service;
        _config = config;
    }

    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularRutaPublica([FromBody] RouteRequestDto dto, [FromHeader(Name = "x-api-key")] string apiKey)
    {
        var expectedKey = _config["ApiKeys:PublicRouteKey"];
        if (apiKey != expectedKey)
        {
            return Unauthorized("Invalid API Key");
        }

        var resultado = await _service.CalcularRutaAsync(dto, Guid.Empty);
        return Ok(resultado);
    }
}
