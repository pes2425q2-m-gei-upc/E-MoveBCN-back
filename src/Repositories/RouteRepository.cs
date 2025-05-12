using src.Dto.Route;
using System.Threading.Tasks;
using Entity;
using src.Entity.Route;



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
}