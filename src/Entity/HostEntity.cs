using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class HostEntity
{
    public Guid HostId { get; set; } 
    public string HostName { get; set; }
    public string HostAddress { get; set; }
    public string HostLocality { get; set; }
    public string HostPostalCode { get; set; }
    public string CountryCode { get; set; }
    public string LanguageCode { get; set; }
    public string OperatorPhone { get; set; }
    public string OperatorWebsite { get; set; }
    
    public string LocationId { get; set; }
    public LocationEntity Location { get; set; }
}
