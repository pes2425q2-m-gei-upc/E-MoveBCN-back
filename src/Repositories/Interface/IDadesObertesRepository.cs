using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;

namespace Repositories.Interface;

public interface IDadesObertesRepository
{
    List<StationDto> GetAllStations();
    Task AddLocationAsync(LocationDto location);
    Task AddHostAsync(HostDto host);
    Task AddStationAsync(StationDto station);
    Task AddPortAsync(PortDto port);
}
