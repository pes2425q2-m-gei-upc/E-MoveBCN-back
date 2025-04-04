using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;

namespace Services.Interface;

public interface IChargingStationsService
{
    Task<List<ChargingStationDto>> GetAllChargingStationsAsync();

    Task FetchAndStoreChargingStationsAsync();
}