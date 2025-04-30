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
    private readonly ITmbService _tmbService;

    public UbicationService(IUserRepository userRepository, IUbicationRepository ubicationRepository, 
                            IBicingStationRepository bicingStationRepository,
                            IChargingStationsRepository chargingStationsRepository,
                            ITmbService tmbService)
    {
        _bicingStationRepository = bicingStationRepository;
        _userRepository = userRepository;
        _ubicationRepository = ubicationRepository;
        _chargingStationsRepository = chargingStationsRepository;
        _tmbService = tmbService;
    }

    public async Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string username)
    {
        var savedUbications = await _ubicationRepository.GetUbicationsByUserIdAsync(username);
        return savedUbications;
    }
    public async Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication)
    {
        var user = await _userRepository.GetUserByUsername(savedUbication.Username);
        if (user == null)
        {
            return false;
        }
        var result = await _ubicationRepository.SaveUbicationAsync(savedUbication);
        return result;
    }
    
    public async Task<bool> DeleteUbication(UbicationInfoDto ubicationDelete)
    {
        var user = await _userRepository.GetUserByUsername(ubicationDelete.Username);
        if (user == null)
        {
            return false;
        }
        var result = await _ubicationRepository.DeleteUbication(ubicationDelete);
        return result;
    }
    public async Task<object> GetUbicationDetails(int ubicationId, string stationType)
    {
    return stationType switch
    {
      UbicationTypeConstants.BICING => await _bicingStationRepository.GetBicingStationDetails(ubicationId),
      UbicationTypeConstants.BUS => await _tmbService.GetBusByIdAsync(ubicationId),
      UbicationTypeConstants.METRO => await _tmbService.GetMetroByIdAsync(ubicationId),
      UbicationTypeConstants.CHARGING => await _chargingStationsRepository.GetChargingStationDetails(ubicationId),
      _ => null,
    };
  }
    public async Task<bool> UpdateUbication(UbicationInfoDto savedUbication)
    {
        var user = await _userRepository.GetUserByUsername(savedUbication.Username);
        if (user == null)
        {
            return false;
        }
        var result = await _ubicationRepository.UpdateUbication(savedUbication);
        return result;
    }
}