using System.Collections.Generic;
using System.Threading.Tasks;
using plantilla.Web.src.Services.Interface;
using Repositories.Interface;

namespace Services;

public class UbicationService : IUbicationService
{
    private readonly IUbicationRepository _ubicationRepository;
    private readonly IUserRepository _userRepository;

    public UbicationService(IUserRepository userRepository, IUbicationRepository ubicationRepository)
    {
        _userRepository = userRepository;
        _ubicationRepository = ubicationRepository;
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
    
}