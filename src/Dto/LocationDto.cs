using System;
using System.Collections.Generic;

public class LocationDto
{
    public string LocationId { get; set; }
    public string NetworkBrandName { get; set; }
    public string NetworkName { get; set; }
    public string OperatorPhone { get; set; }

    public string OperatorWebsite { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string AddressString { get; set; }
    public string Locality { get; set; }
    public string AdminArea { get; set; }
    public string PostalCode { get; set; }
    public string CountryCode { get; set; }
    public string LanguageCode { get; set; }
    public string AccessRestriction { get; set; }
    public bool OnstreetLocation { get; set; }
    public DateTime LastUpdated { get; set; }
    public int WeekdayBegin { get; set; }
    public int WeekdayEnd { get; set; }
    public TimeSpan HourBegin { get; set; }
    public TimeSpan HourEnd { get; set; }
    
    public ICollection<HostEntity> Hosts { get; set; }
    public ICollection<StationEntity> Stations { get; set; }
}