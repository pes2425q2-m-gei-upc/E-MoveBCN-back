using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public enum StationType { CHARGING, BICING, BUS, METRO }

    public class SavedUbicationEntity
    {
        public Guid UbicationId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }

        public virtual UserEntity User { get; set; }
        public StationType StationType { get; set; }
        public string ChargingStationId { get; set; }
        public virtual StationEntity ChargingStation { get; set; }
        public int? BicingStationId { get; set; }
        public virtual BicingStationEntity BicingStation { get; set; }
        public string DisplayName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? BusStopId { get; set; }
        public string BusStreetName { get; set; }
        public string BusDistrictName { get; set; }
        public int? MetroStationId { get; set; }
        public int? MetroLineId { get; set; }
        public string MetroLineName { get; set; }
        public string MetroLineColor { get; set; }
        public bool IsFavorite { get; set; } = false;
        public int? RatingValue { get; set; }
        public string RatingComment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}