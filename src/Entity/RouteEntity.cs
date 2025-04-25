using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

public class RouteEntity
{
    public  required Guid Id { get; set; }

    public required double OriginLat { get; set; }
    public required double OriginLng {get; set;}
    public required double DestinationLat {get; set;}
    public required double DestinationLng { get; set; }
    public required string Mean { get; set; }
    public required string Preference { get; set; }

    public required float Distance { get; set; }
    public required float Duration { get; set; }
    public required string GeometryJson { get; set; }

    public required string InstructionsJson { get; set; }
}
