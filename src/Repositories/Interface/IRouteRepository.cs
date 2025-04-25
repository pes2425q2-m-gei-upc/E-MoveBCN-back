using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRouteRepository
{
    Task GuardarRutaAsync(RouteEntity ruta);
}