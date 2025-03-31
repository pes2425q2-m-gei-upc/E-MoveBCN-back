using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;   
using Dto;
namespace Repositories.Interface;

public interface IStateBicingRepository
{
    Task<List<StateBicingDto>> GetAllStateBicingStations();
    Task BulkInsertAsync(
        List<StateBicingEntity> statebicing
    );
}