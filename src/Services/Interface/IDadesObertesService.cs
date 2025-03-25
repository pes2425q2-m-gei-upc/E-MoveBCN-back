using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;

namespace Services.Interface;

public interface IDadesObertesService
{
    Task<List<StationDto>> GetAllStationsAsync();

    Task FetchAndStoreChargingStationsAsync();
}