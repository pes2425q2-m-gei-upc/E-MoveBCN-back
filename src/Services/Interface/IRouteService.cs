using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Dto.Route;

public interface IRouteService
{
  public Task<RouteResponseDto> CalcularRutaAsync(RouteRequestDto request, Guid usuarioId);
  public Task<bool> SaveRoute(RouteDto route);
  public Task<bool> DeleteRoute(string routeId);
  public Task<bool> PublishRoute(PublishedRouteDto publishedRouteDto);
  public Task<bool> DeletePublishedRoute(string routeId);
}
