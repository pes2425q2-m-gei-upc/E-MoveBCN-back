using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public enum StationType { CHARGING, BICING, BUS, METRO }

    public class SavedUbicationEntity
    {
        public required Guid UbicationId { get; set; } 


        public required string Username { get; set; }


        public required virtual UserEntity User { get; set; }
        public required StationType StationType { get; set; }
        public string ChargingStationId { get; set; }
        public virtual StationEntity ChargingStation { get; set; }
        public int? BicingStationId { get; set; }
        public virtual BicingStationEntity BicingStation { get; set; }
        public required string DisplayName { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
        public int? BusStopId { get; set; }
        public string? BusStreetName { get; set; }
        public string? BusDistrictName { get; set; }
        public int? MetroStationId { get; set; }
        public int? MetroLineId { get; set; }
        public string? MetroLineName { get; set; }
        public string? MetroLineColor { get; set; }
        public required bool IsFavorite { get; set; } = false;
        public int? RatingValue { get; set; }
        public string? RatingComment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}