using Repositories.Interface;
using Entity;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;



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
        await _dbContext.SaveChangesAsync();
    }
}
