using src.Dto.Route;
using System.Threading.Tasks;
using Entity;
using src.Entity.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;



public class RouteRepository : IRouteRepository
{
  private readonly ApiDbContext _dbContext;
  private readonly IMapper _mapper;

  public RouteRepository(ApiDbContext dbContext, IMapper mapper)
  {
    _mapper = mapper;
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
  public async Task<bool> PublishRoute(PublishedRouteDto publishedRouteDto)
  {
    var ruta = await _dbContext.Routes.FindAsync(Guid.Parse(publishedRouteDto.RouteId)).ConfigureAwait(false);
    if (ruta == null)
    {
      return false;
    }
    await _dbContext.PublishedRoutes.AddAsync(new PublishedRouteEntity
    {
      RouteId = Guid.Parse(publishedRouteDto.RouteId),
      Date = publishedRouteDto.Date,
      AvailableSeats = publishedRouteDto.AvailableSeats,
    }).ConfigureAwait(false);
    return await _dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> DeletePublishedRoute(string routeId)
  {
    var publishedRoute = await _dbContext.PublishedRoutes.FindAsync(Guid.Parse(routeId)).ConfigureAwait(false);
    if (publishedRoute == null)
    {
      return false;
    }
    _dbContext.PublishedRoutes.Remove(publishedRoute);
    return await _dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<List<PublishedRouteDto>> GetRoutesNearAsync(double lat, double lon, double radiusInMeters)
{
    const double EarthRadius = 6371000;

    // Traer todas las rutas publicadas y sus rutas base
    var publishedRoutes = await _dbContext.PublishedRoutes
        .Include(p => p.RouteIdNavigation)
        .ToListAsync().ConfigureAwait(false);

    // Calcular distancia en memoria
    var result = publishedRoutes
        .Where(p =>
        {
            var originLat = p.RouteIdNavigation.OriginLat;
            var originLng = p.RouteIdNavigation.OriginLng;

            double distance = EarthRadius * Math.Acos(
                Math.Cos(DegToRad(lat)) * Math.Cos(DegToRad(originLat)) *
                Math.Cos(DegToRad(originLng - lon)) +
                Math.Sin(DegToRad(lat)) * Math.Sin(DegToRad(originLat))
            );

            return distance < radiusInMeters;
        })
        .ToList();
    return _mapper.Map<List<PublishedRouteDto>>(result);
}
  private static double DegToRad(double deg) => deg * (Math.PI / 180);
}