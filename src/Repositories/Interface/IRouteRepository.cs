using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Route;
using Entity.Route;
namespace Repositories.Interface;

public interface IRouteRepository
{
  Task<bool> GuardarRutaAsync(RouteEntity ruta);
  Task<bool> DeleteRoute(string rutaId);
  Task<bool> PublishRoute(PublishedRouteDto publishedRouteDto);
  Task<bool> DeletePublishedRoute(string routeId);
  Task<List<PublishedRouteDto>> GetRoutesNearAsync(double lat, double lon, double radiusInMeters);
  Task<List<RouteDto>> GetSavedRoute(string userId);
  
  Task<List<PublishedRouteDto>> GetPublishedRoutesByUserIdAsync(string userId);

}
