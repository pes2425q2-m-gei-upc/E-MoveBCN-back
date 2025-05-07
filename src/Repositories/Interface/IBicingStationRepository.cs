using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;
using Entity;
namespace Repositories.Interface;

public interface IBicingStationRepository
{
  Task<List<BicingStationDto>> GetAllBicingStations();
  Task BulkInsertAsync(List<BicingStationEntity> bicingstations);
  Task<BicingStationDto> GetBicingStationDetails(int id);
}
