using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Services.Interface;

public interface IBicingStationService
{
    Task<List<BicingStationDto>> GetAllBicingStationsAsync();
    Task FetchAndStoreBicingStationsAsync();

    
}