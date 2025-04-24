using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;
namespace Services.Interface;

public interface IUbicationService
{
    Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string userId);
}