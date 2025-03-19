using System.Collections.Generic;

public class HostDto
{
    public string HostId { get; set; }
    public string HostName { get; set; }
    public string HostAddress { get; set; }
    public string HostLocality { get; set; }
    public string HostPostalCode { get; set; }
    public string CountryCode { get; set; }
    public string LanguageCode { get; set; }
    public string OperatorPhone { get; set; }
    public string OperatorWebsite { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string Notes { get; set; }
    public bool Reservable { get; set; }
    public List<AuthenticationDto> Authentications { get; set; }
}