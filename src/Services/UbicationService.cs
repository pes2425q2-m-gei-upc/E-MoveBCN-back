using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Dto;
using plantilla.Web.src.Services.Interface;
using Repositories.Interface;
using Services.Interface;
using src.Dto;
using src.Dto.Air;
using src.Helpers.Interface;

namespace Services;

public class UbicationService : IUbicationService
{
  private readonly IUbicationRepository _ubicationRepository;
  private readonly IUserRepository _userRepository;
  private readonly IBicingStationRepository _bicingStationRepository;
  private readonly IChargingStationsRepository _chargingStationsRepository;
  private readonly IAireLliureHelper _aireLliureHelper;
  private readonly IStateBicingRepository _stateBicingRepository;
  private readonly ITmbService _tmbService;

  public UbicationService(IUserRepository userRepository, IUbicationRepository ubicationRepository,
                          IBicingStationRepository bicingStationRepository,
                          IChargingStationsRepository chargingStationsRepository,
                          IAireLliureHelper aireLliureHelper,
                          IStateBicingRepository stateBicingRepository,
                          ITmbService tmbService)
  {
    _bicingStationRepository = bicingStationRepository;
    _userRepository = userRepository;
    _ubicationRepository = ubicationRepository;
    _stateBicingRepository = stateBicingRepository;
    _chargingStationsRepository = chargingStationsRepository;
    _aireLliureHelper = aireLliureHelper;
    _tmbService = tmbService;
  }

  public async Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string userEmail)
  {
    var savedUbications = await _ubicationRepository.GetUbicationsByEmailAsync(userEmail).ConfigureAwait(false);
    return savedUbications;
  }
  public async Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication)
  {
    var user = await _userRepository.GetUserByEmailAsync(savedUbication.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await _ubicationRepository.SaveUbicationAsync(savedUbication).ConfigureAwait(false);
    return result;
  }

  public async Task<bool> DeleteUbication(UbicationInfoDto ubicationDelete)
  {
    var user = await _userRepository.GetUserByEmailAsync(ubicationDelete.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await _ubicationRepository.DeleteUbication(ubicationDelete).ConfigureAwait(false);
    return result;
  }
  public async Task<(object,double?)> GetUbicationDetails(int ubicationId, string stationType)
  {
    var result = stationType switch
    {
      UbicationTypeConstants.BICING => (object?)await GetBicingStationWithStatus(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.BUS => (object?)await _tmbService.GetBusByIdAsync(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.METRO => (object?)await _tmbService.GetMetroByIdAsync(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.CHARGING => (object?)await _chargingStationsRepository.GetChargingStationDetails(ubicationId).ConfigureAwait(false),
      _ => (object?)null,
    };
    var latitude = stationType switch
    {
      UbicationTypeConstants.BICING => ((BicingStationWithStatusDto)result).StationInfo.Latitude,
      UbicationTypeConstants.BUS => ((BusDto)result).Latitude,
      UbicationTypeConstants.METRO => ((MetroDto)result).Latitude,
      UbicationTypeConstants.CHARGING => ((ChargingStationDto)result).LocationLatitude,
      _ => 0.0
    };
    var longitude = stationType switch
    {
      UbicationTypeConstants.BICING => ((BicingStationWithStatusDto)result).StationInfo.Longitude,
      UbicationTypeConstants.BUS => ((BusDto)result).Longitude,
      UbicationTypeConstants.METRO => ((MetroDto)result).Longitude,
      UbicationTypeConstants.CHARGING => ((ChargingStationDto)result).LocationLongitude,
      _ => 0.0
    };
    var sensors = await _aireLliureHelper.GetAllAirSensorsAsync().ConfigureAwait(false);
    var airQuality = _aireLliureHelper.FindClosestSensor(sensors, longitude, latitude);
    return (result, airQuality);

  }
  public async Task<bool> UpdateUbication(UbicationInfoDto savedUbication)
  {
    var user = await _userRepository.GetUserByEmailAsync(savedUbication.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await _ubicationRepository.UpdateUbication(savedUbication).ConfigureAwait(false);
    return result;
  }

  private async Task<BicingStationWithStatusDto?> GetBicingStationWithStatus(int stationId)
  {
    var info = await _bicingStationRepository.GetBicingStationDetails(stationId).ConfigureAwait(false);
    var status = await _stateBicingRepository.GetStateBicingById(stationId).ConfigureAwait(false);

    return new BicingStationWithStatusDto
    {
        StationInfo = info,
        RealTimeStatus = status
    };
  }

}
