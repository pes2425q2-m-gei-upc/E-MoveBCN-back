using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Repositories.Interface;



public class RouteRepository : IRouteRepository
{
  private readonly ApiDbContext _dbContext;

  public RouteRepository(ApiDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task GuardarRutaAsync(RouteEntity ruta)
  {
    _dbContext.Routes.Add(ruta); // to-do: guardar en UserRoutes
    await _dbContext.SaveChangesAsync().ConfigureAwait(false);
  }
}
