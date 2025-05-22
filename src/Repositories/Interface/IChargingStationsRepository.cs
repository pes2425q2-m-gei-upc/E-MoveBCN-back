using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Bicing;
using Entity.Bicing;
namespace Repositories.Interface;
public interface IChargingStationsRepository
{
  Task<List<ChargingStationDto>> GetAllChargingStations();
  Task BulkInsertAsync(
      List<LocationEntity> locations,
      List<HostEntity> hosts,
      List<StationEntity> stations,
      List<PortEntity> ports
  );
  Task<ChargingStationDto> GetChargingStationDetails(int ubicationId);
}
