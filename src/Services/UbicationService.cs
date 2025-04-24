using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Interface;
using Services.Interface;

namespace Services;

public class UbicationService : IUbicationService
{
    private readonly IUbicationRepository _ubicationRepository;
    public UbicationService(IUbicationRepository ubicationRepository)
    {
        _ubicationRepository = ubicationRepository;
    }

    public async Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string userId)
    {
        var savedUbications = await _ubicationRepository.GetUbicationsByUserIdAsync(userId);
        return savedUbications;
    }
    
}