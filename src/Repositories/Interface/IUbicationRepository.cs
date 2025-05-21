using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Ubication;
namespace Repositories.Interface;
public interface IUbicationRepository
{
  Task<List<SavedUbicationDto>> GetUbicationsByEmailAsync(string userEmail);
  Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication);
  Task<bool> DeleteUbication(UbicationInfoDto ubicationDelete);
  Task<bool> UpdateUbication(UbicationInfoDto savedUbication);
}
