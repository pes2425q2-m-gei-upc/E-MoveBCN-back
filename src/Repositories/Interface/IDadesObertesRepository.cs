using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;

namespace Repositories.Interface;

public interface IDadesObertesRepository
{
    Task<List<StationDto>> GetAllStations();
    Task BulkInsertAsync(
        List<LocationEntity> locations,
        List<HostEntity> hosts,
        List<StationEntity> stations,
        List<PortEntity> ports
    );
}
