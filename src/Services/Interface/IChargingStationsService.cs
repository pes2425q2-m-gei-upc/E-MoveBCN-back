using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Bicing;
namespace Services.Interface;
public interface IChargingStationsService
{
  Task<List<ChargingStationDto>> GetAllChargingStationsAsync();

  Task FetchAndStoreChargingStationsAsync();
}
