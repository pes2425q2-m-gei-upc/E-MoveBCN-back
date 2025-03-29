using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;   
using Dto;
namespace Repositories.Interface;

public interface IBicingStationRepository
{
    Task<List<BicingStationDto>> GetAllBicingStations();
    Task BulkInsertAsync(
        List<BicingStationEntity> bicingstations
    );
}