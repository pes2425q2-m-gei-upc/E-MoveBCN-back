using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Repositories.Interface;

public interface IUbicationRepository
{
    Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string username);
    Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication);
    Task<bool> DeleteUbication(UbicationDeleteDto ubicationDelete);
}