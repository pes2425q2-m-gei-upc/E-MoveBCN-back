using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using src.Dto.Route;
using src.Entity.Route;

public interface IRouteRepository
{
  Task<bool> GuardarRutaAsync(RouteEntity ruta);
  Task<bool> DeleteRoute(string rutaId);
  Task<bool> PublishRoute(PublishedRouteDto publishedRouteDto);
  Task<bool> DeletePublishedRoute(string routeId);
}
