using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Services.Interface;

public interface IStateBicingService
{
    Task<List<BicingStationDto>> GetAllStateBicingStationsAsync();
    Task FetchAndStoreStateBicingStationsAsync();

    
}