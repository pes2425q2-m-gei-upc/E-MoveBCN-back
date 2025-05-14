using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;
using Microsoft.Extensions.ObjectPool;
namespace plantilla.Web.src.Services.Interface;

public interface IUbicationService
{
  Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string userEmail);
  Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication);
  Task<bool> DeleteUbication(UbicationInfoDto ubicationDeleteDto);
  Task<bool> UpdateUbication(UbicationInfoDto savedUbication);
  Task<(Object,double?)> GetUbicationDetails(int ubicationId, string stationType);
}
