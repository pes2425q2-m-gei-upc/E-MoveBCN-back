using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

public class RouteEntity
{
    [Key]
    public Guid Id { get; set; }

    public double OriginLat { get; set; }
    public double OriginLng {get; set;}
    public double DestinationLat {get; set;}
    public double DestinationLng { get; set; }
    public string Mean { get; set; }
    public string Preference { get; set; }

    public float Distance { get; set; }
    public float Duration { get; set; }

     [Column(TypeName = "jsonb")]
    public string GeometryJson { get; set; }
}
