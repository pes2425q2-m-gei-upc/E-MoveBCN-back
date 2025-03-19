using System.Collections.Generic;

public class StationDto
{
    public string StationId { get; set; }
    public string StationLabel { get; set; }
    public float StationLatitude { get; set; }
    public float StationLongitude { get; set; }
    public string Notes { get; set; }
    public bool Reservable { get; set; }
    public List<AuthenticationDto> Authentications { get; set; }    
}