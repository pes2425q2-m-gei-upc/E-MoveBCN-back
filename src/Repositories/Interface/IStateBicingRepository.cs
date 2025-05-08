using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;
using Entity;
namespace Repositories.Interface;

public interface IStateBicingRepository
{
  Task<List<StateBicingDto>> GetAllStateBicingStations();

  Task<StateBicingDto> GetStateBicingById(int stationId);
  Task BulkInsertAsync(
      List<StateBicingEntity> statebicing
  );


}
