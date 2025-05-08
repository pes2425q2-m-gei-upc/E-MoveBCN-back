using System.Collections.Generic;

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


  public List<PortDto> Ports { get; set; }
}
