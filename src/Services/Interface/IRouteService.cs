using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Route;
using Microsoft.Extensions.Localization;
namespace Services.Interface;

public interface IRouteService
{
  public Task<RouteResponseDto> CalcularRutaAsync(RouteRequestDto request);
  public Task<bool> SaveRoute(RouteDto route);
  public Task<bool> DeleteRoute(string routeId);
  public Task<bool> PublishRoute(PublishedRouteDto publishedRouteDto);
  public Task<bool> DeletePublishedRoute(string routeId);
  public Task<List<PublishedRouteDto>> GetRoutesNearAsync(double lat, double lon, double radiusInMeters);
  public Task<List<RouteDto>> GetSavedRoute(string userId);
  public Task<string> GetStreetNameAsync(double lat, double lon);
  
  Task<List<PublishedRouteDto>> GetPublishedRoutesByUserIdAsync(string userId);

}
