using Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRouteService
{
    public Task<RouteResponseDto> CalcularRutaAsync(RouteRequestDto request, Guid usuarioId);
}
