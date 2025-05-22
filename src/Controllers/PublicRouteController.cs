using System.Threading.Tasks;
using Dto.Route;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interface;
namespace Controllers;
[ApiController]
[Route("api/public")]
public class PublicRutasController(IRouteService service, IConfiguration config) : ControllerBase
{
  private readonly IRouteService _service = service;
  private readonly IConfiguration _config = config;

  [HttpPost("calcular")]
  public async Task<IActionResult> CalcularRutaPublica([FromBody] RouteRequestDto dto, [FromHeader(Name = "x-api-key")] string apiKey)
  {
    var expectedKey = this._config["ApiKeys:PublicRouteKey"];
    if (apiKey != expectedKey)
    {
      return Unauthorized("Invalid API Key");
    }

    var resultado = await this._service.CalcularRutaAsync(dto).ConfigureAwait(false);
    return Ok(resultado);
  }
}
