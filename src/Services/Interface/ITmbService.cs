using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Services.Interface;

    public interface ITmbService
    {
        Task<List<MetroDto>> GetAllMetrosAsync();
        Task<List<BusDto>> GetAllBusAsync();
    }