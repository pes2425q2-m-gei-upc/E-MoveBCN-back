using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Air;

namespace Helpers.Interface;

public interface IAireLliureHelper
{
  Task<List<AirSensorDto>> GetAllAirSensorsAsync();
  double? FindClosestSensor(List<AirSensorDto> sensors, double targetLat, double targetLon);
}
