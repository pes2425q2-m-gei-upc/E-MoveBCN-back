using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Bicing;
namespace Services.Interface;
public interface IBicingStationService
{
  Task<List<BicingStationDto>> GetAllBicingStations();
  Task FetchAndStoreBicingStationsAsync();
}
