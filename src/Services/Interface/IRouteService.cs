using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

public interface IRouteService
{
  public Task<RouteResponseDto> CalcularRutaAsync(RouteRequestDto request, Guid usuarioId);
}
