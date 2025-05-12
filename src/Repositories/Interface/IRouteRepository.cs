using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using src.Entity.Route;

public interface IRouteRepository
{
  Task<bool> GuardarRutaAsync(RouteEntity ruta);
}
