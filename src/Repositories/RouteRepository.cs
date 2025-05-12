using src.Dto.Route;
using System.Threading.Tasks;
using Entity;
using src.Entity.Route;
using System;



public class RouteRepository : IRouteRepository
{
  private readonly ApiDbContext _dbContext;

  public RouteRepository(ApiDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<bool> GuardarRutaAsync(RouteEntity ruta)
  {
    _dbContext.Routes.Add(ruta); // to-do: guardar en UserRoutes
    return await _dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> DeleteRoute(string rutaId)
  {
    var ruta = await _dbContext.Routes.FindAsync(Guid.Parse(rutaId)).ConfigureAwait(false);
    if (ruta == null)
    {
      return false;
    }
    _dbContext.Routes.Remove(ruta);
    return await _dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
}