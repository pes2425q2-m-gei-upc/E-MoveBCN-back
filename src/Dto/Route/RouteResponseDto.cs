#nullable enable
using System.Collections.Generic;
namespace Dto.Route;
public class RouteResponseDto
{
  public required string RouteId { get; set; }
  public double Distance { get; set; }
  public double Duration { get; set; }

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Necesario para deserialización o asignación completa")]
  public required List<double[]> Geometry { get; set; }

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Necesario para deserialización o asignación completa")]
  public required List<RouteInstructionDto> Instructions { get; set; }
  public string? OriginStreetName { get; set; }
  public string? DestinationStreetName { get; set; }
}
