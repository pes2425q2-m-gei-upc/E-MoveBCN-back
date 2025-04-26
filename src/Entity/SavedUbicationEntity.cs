using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class SavedUbicationEntity
    {
        public required int UbicationId { get; set; } 
        public required string Username { get; set; }
        public required string StationType { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
        public int? Valoration { get; set; }
        public string? Comment { get; set; }

    }
}