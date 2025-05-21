#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Dto.Air;
using Dto.Bicing;
using Dto.Tmb;
using Dto.Ubication;
using Helpers.Interface;
using Repositories.Interface;
using Services.Interface;

namespace Services;

public class UbicationService(IUserRepository userRepository, IUbicationRepository ubicationRepository,
                        IBicingStationRepository bicingStationRepository,
                        IChargingStationsRepository chargingStationsRepository,
                        IAireLliureHelper aireLliureHelper,
                        IStateBicingRepository stateBicingRepository,
                        ITmbService tmbService) : IUbicationService
{
  private readonly IUbicationRepository _ubicationRepository = ubicationRepository;
  private readonly IUserRepository _userRepository = userRepository;
  private readonly IBicingStationRepository _bicingStationRepository = bicingStationRepository;
  private readonly IChargingStationsRepository _chargingStationsRepository = chargingStationsRepository;
  private readonly IAireLliureHelper _aireLliureHelper = aireLliureHelper;
  private readonly IStateBicingRepository _stateBicingRepository = stateBicingRepository;
  private readonly ITmbService _tmbService = tmbService;

  public async Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string userEmail)
  {
    var savedUbications = await this._ubicationRepository.GetUbicationsByEmailAsync(userEmail).ConfigureAwait(false);
    return savedUbications;
  }
  public async Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication)
  {
    if (savedUbication == null)
    {
      throw new ArgumentNullException(nameof(savedUbication));
    }
    var user = await this._userRepository.GetUserByEmailAsync(savedUbication.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await this._ubicationRepository.SaveUbicationAsync(savedUbication).ConfigureAwait(false);
    return result;
  }

  public async Task<bool> DeleteUbication(UbicationInfoDto ubicationDelete)
  {
    if (ubicationDelete == null)
    {
      throw new ArgumentNullException(nameof(ubicationDelete));
    }
    var user = await this._userRepository.GetUserByEmailAsync(ubicationDelete.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await this._ubicationRepository.DeleteUbication(ubicationDelete).ConfigureAwait(false);
    return result;
  }
  public async Task<(object, double?)> GetUbicationDetails(int ubicationId, string stationType)
  {
    object? result = stationType switch
    {
      UbicationTypeConstants.BICING => await GetBicingStationWithStatus(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.BUS => await this._tmbService.GetBusByIdAsync(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.METRO => await this._tmbService.GetMetroByIdAsync(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.CHARGING => await this._chargingStationsRepository.GetChargingStationDetails(ubicationId).ConfigureAwait(false),
      _ => null,
    };
    double latitude = 0.0;
    double longitude = 0.0;

    switch (stationType)
    {
      case UbicationTypeConstants.BICING:
        var bicing = result as BicingStationWithStatusDto;
        if (bicing?.StationInfo != null)
        {
          latitude = bicing.StationInfo.Latitude;
          longitude = bicing.StationInfo.Longitude;
        }
        break;
      case UbicationTypeConstants.BUS:
        if (result is BusDto bus)
        {
          latitude = bus.Latitude;
          longitude = bus.Longitude;
        }
        break;
      case UbicationTypeConstants.METRO:
        if (result is MetroDto metro)
        {
          latitude = metro.Latitude;
          longitude = metro.Longitude;
        }
        break;
      case UbicationTypeConstants.CHARGING:
        if (result is ChargingStationDto charging)
        {
          latitude = charging.LocationLatitude;
          longitude = charging.LocationLongitude;
        }
        break;
      default:
        latitude = 0.0;
        longitude = 0.0;
        break;
    }

    var sensors = await this._aireLliureHelper.GetAllAirSensorsAsync().ConfigureAwait(false);
    var airQuality = this._aireLliureHelper.FindClosestSensor(sensors, longitude, latitude);
    return (result, airQuality)!;

  }
  public async Task<bool> UpdateUbication(UbicationInfoDto savedUbication)
  {
    if (savedUbication == null)
    {
      throw new ArgumentNullException(nameof(savedUbication));
    }
    var user = await this._userRepository.GetUserByEmailAsync(savedUbication.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await this._ubicationRepository.UpdateUbication(savedUbication).ConfigureAwait(false);
    return result;
  }

  private async Task<BicingStationWithStatusDto?> GetBicingStationWithStatus(int stationId)
  {
    var info = await this._bicingStationRepository.GetBicingStationDetails(stationId).ConfigureAwait(false);
    var status = await this._stateBicingRepository.GetStateBicingById(stationId).ConfigureAwait(false);

    return new BicingStationWithStatusDto
    {
      StationInfo = info,
      RealTimeStatus = status
    };
  }

}
