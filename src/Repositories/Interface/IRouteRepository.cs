using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;

public interface IRouteRepository
{
  Task GuardarRutaAsync(RouteEntity ruta);
}
