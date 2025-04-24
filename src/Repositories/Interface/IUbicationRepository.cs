using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interface;

public interface IUbicationRepository
{
    Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string userId);
}