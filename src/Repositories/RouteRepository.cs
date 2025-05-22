using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto.Route;
using Entity;
using Entity.Route;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
namespace Repositories;
public class RouteRepository(ApiDbContext dbContext, IMapper mapper) : IRouteRepository
{
  private readonly ApiDbContext _dbContext = dbContext;
  private readonly IMapper _mapper = mapper;

  public async Task<bool> GuardarRutaAsync(RouteEntity ruta)
  {
    this._dbContext.Routes.Add(ruta); // to-do: guardar en UserRoutes
    return await this._dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> DeleteRoute(string rutaId)
  {
    var ruta = await this._dbContext.Routes.FindAsync(Guid.Parse(rutaId, System.Globalization.CultureInfo.InvariantCulture)).ConfigureAwait(false);
    if (ruta == null)
    {
      return false;
    }
    this._dbContext.Routes.Remove(ruta);
    return await this._dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> PublishRoute(PublishedRouteDto publishedRouteDto)
  {
    ArgumentNullException.ThrowIfNull(publishedRouteDto);
    var ruta = await this._dbContext.Routes.FindAsync(Guid.Parse(publishedRouteDto.RouteId, System.Globalization.CultureInfo.InvariantCulture)).ConfigureAwait(false);
    if (ruta == null)
    {
      return false;
    }
    await this._dbContext.PublishedRoutes.AddAsync(new PublishedRouteEntity
    {
      RouteId = Guid.Parse(publishedRouteDto.RouteId, System.Globalization.CultureInfo.InvariantCulture),
      Date = publishedRouteDto.Date,
      AvailableSeats = publishedRouteDto.AvailableSeats,
    }).ConfigureAwait(false);
    return await this._dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> DeletePublishedRoute(string routeId)
  {
    var publishedRoute = await this._dbContext.PublishedRoutes.FindAsync(Guid.Parse(routeId, System.Globalization.CultureInfo.InvariantCulture)).ConfigureAwait(false);
    if (publishedRoute == null)
    {
      return false;
    }
    this._dbContext.PublishedRoutes.Remove(publishedRoute);
    return await this._dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<List<PublishedRouteDto>> GetRoutesNearAsync(double lat, double lon, double radiusInMeters)
  {
    const double EarthRadius = 6371000;

    // Traer todas las rutas publicadas y sus rutas base
    var publishedRoutes = await this._dbContext.PublishedRoutes
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
    return this._mapper.Map<List<PublishedRouteDto>>(result);
  }
  private static double DegToRad(double deg) => deg * (Math.PI / 180);

  public async Task<List<RouteDto>> GetSavedRoute(string userId)
  {
    var entities = await this._dbContext.Routes
      .Where(u => u.UserId == Guid.Parse(userId, System.Globalization.CultureInfo.InvariantCulture))
      .ToListAsync().ConfigureAwait(false);
    return this._mapper.Map<List<RouteDto>>(entities);
  }
}
