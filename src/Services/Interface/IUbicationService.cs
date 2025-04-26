using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;
namespace plantilla.Web.src.Services.Interface;

public interface IUbicationService
{
  Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string username);
  Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication);
  Task<bool> DeleteUbication(UbicationInfoDto ubicationDeleteDto);
  Task<bool> UpdateUbication(UbicationInfoDto savedUbication);
}