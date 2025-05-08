using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Dto;
using plantilla.Web.src.Services.Interface;
using Repositories.Interface;
using Services.Interface;

namespace Services;

public class UbicationService : IUbicationService
{
  private readonly IUbicationRepository _ubicationRepository;
  private readonly IUserRepository _userRepository;
  private readonly IBicingStationRepository _bicingStationRepository;
  private readonly IChargingStationsRepository _chargingStationsRepository;

  private readonly IStateBicingRepository _stateBicingRepository;
  private readonly ITmbService _tmbService;

  public UbicationService(IUserRepository userRepository, IUbicationRepository ubicationRepository,
                          IBicingStationRepository bicingStationRepository,
                          IChargingStationsRepository chargingStationsRepository,
                          IStateBicingRepository stateBicingRepository,
                          ITmbService tmbService)
  {
    _bicingStationRepository = bicingStationRepository;
    _userRepository = userRepository;
    _ubicationRepository = ubicationRepository;
    _stateBicingRepository = stateBicingRepository;
    _chargingStationsRepository = chargingStationsRepository;
    _tmbService = tmbService;
  }

  public async Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string userEmail)
  {
    var savedUbications = await _ubicationRepository.GetUbicationsByUserIdAsync(userEmail).ConfigureAwait(false);
    return savedUbications;
  }
  public async Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication)
  {
    var user = await _userRepository.GetUserByUsername(savedUbication.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await _ubicationRepository.SaveUbicationAsync(savedUbication).ConfigureAwait(false);
    return result;
  }

  public async Task<bool> DeleteUbication(UbicationInfoDto ubicationDelete)
  {
    var user = await _userRepository.GetUserByUsername(ubicationDelete.Username).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await _ubicationRepository.DeleteUbication(ubicationDelete).ConfigureAwait(false);
    return result;
  }
  public async Task<object> GetUbicationDetails(int ubicationId, string stationType)
  {
    return stationType switch
    {
      UbicationTypeConstants.BICING => await GetBicingStationWithStatus(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.BUS => await _tmbService.GetBusByIdAsync(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.METRO => await _tmbService.GetMetroByIdAsync(ubicationId).ConfigureAwait(false),
      UbicationTypeConstants.CHARGING => await _chargingStationsRepository.GetChargingStationDetails(ubicationId).ConfigureAwait(false),
      _ => null,
    };
  }
  public async Task<bool> UpdateUbication(UbicationInfoDto savedUbication)
  {
    var user = await _userRepository.GetUserByUsername(savedUbication.Username).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var result = await _ubicationRepository.UpdateUbication(savedUbication).ConfigureAwait(false);
    return result;
  }

  private async Task<object?> GetBicingStationWithStatus(int stationId)
  {
    var info = await _bicingStationRepository.GetBicingStationDetails(stationId).ConfigureAwait(false);
    var status = await _stateBicingRepository.GetStateBicingById(stationId).ConfigureAwait(false);

    return new
    {
      stationInfo = info,
      realTimeStatus = status
    };
  }

}
