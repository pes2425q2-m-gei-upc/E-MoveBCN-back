using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Dto.Bicing;

/// <summary>
/// DTO that represents a charging station with location, host and ports data.
/// The Ports collection has a public setter because EF Core requires writable properties
/// for projection in LINQ queries (select new ...).
///
/// The warning CA2227 is suppressed intentionally to allow full assignment
/// of the collection during DTO construction in queries.
/// </summary>
public class ChargingStationDto
{
  public string StationId { get; set; }
  public string StationLabel { get; set; }
  public double StationLatitude { get; set; }
  public double StationLongitude { get; set; }
  public string AddressString { get; set; }
  public string Locality { get; set; }
  public string PostalCode { get; set; }
  public double LocationLatitude { get; set; }
  public double LocationLongitude { get; set; }
  public string HostId { get; set; }
  public string HostName { get; set; }
  public string HostPhone { get; set; }
  public string HostWebsite { get; set; }

  /// <summary>
  /// Ports related to the charging station.
  /// SuppressMessage is used to allow collection replacement during projection.
  /// </summary>
  [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Needed for LINQ projection in EF Core")]
  public List<PortDto> Ports { get; set; }
}
