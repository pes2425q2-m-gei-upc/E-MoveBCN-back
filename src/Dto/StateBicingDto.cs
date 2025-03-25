using System;

namespace Dto;

public class StateBicingDto
{
    public required int BicingId { get; set; }
    public required int NumBikesAvailable { get; set; }
    public required int NumBikesAvailableMechanical { get; set; }
    public required int NumBikesAvailableEbike { get; set; }
    public required int NumDocksAvailable { get; set; }
    public required DateTime LastReported { get; set; }
    public required string Status { get; set; }
    public BicingStationDto BicingStation { get; set; }
} 