using System;

namespace Dto;

public class StateBicingDto
{
    public int BicingId { get; set; }
    public int NumBikesAvailable { get; set; }
    public int NumBikesAvailableMechanical { get; set; }
    public int NumBikesAvailableEbike { get; set; }
    public int NumDocksAvailable { get; set; }
    public DateTime LastReported { get; set; }
    public string Status { get; set; }
    public int IsInstalled { get; set; }
    public int IsRenting { get; set; }
} 