#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Tmb;
namespace Services.Interface;
public interface ITmbService
{
  Task<List<MetroDto>> GetAllMetrosAsync();
  Task<List<BusDto>> GetAllBusAsync();
  Task<MetroDto?> GetMetroByIdAsync(int id);
  Task<BusDto?> GetBusByIdAsync(int id);
}
