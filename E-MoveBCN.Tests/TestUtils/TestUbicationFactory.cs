
using System;
using Dto;
namespace TestUtils;
public static class TestUbicationFactory
{
  public static SavedUbicationDto CreateSavedUbicationDto(string email = "user@example.com")
  {
    return new SavedUbicationDto
    {
      UbicationId = 1,
      UserEmail = email,
      StationType = "BICING",
      Latitude = 41.3851,
      Longitude = 2.1734,
      Valoration = 5,
      Comment = "Great place!",
      AirQuality = 42.5
    };
  }

  public static UbicationInfoDto CreateUbicationInfoDto(string email = "user@example.com")
  {
    return new UbicationInfoDto
    {
      UbicationId = 1,
      UserEmail = email,
      StationType = "BICING",
      Valoration = 4,
      Comment = "Good"
    };
  }

  public static BicingStationDto CreateBicingStationDto()
  {
    return new BicingStationDto
    {
      BicingId = 1,
      BicingName = "Station 1",
      Latitude = 41.3851,
      Longitude = 2.1734,
      Altitude = 12.5,
      Address = "Carrer de la Marina",
      CrossStreet = "Gran Via",
      PostCode = "08018",
      Capacity = 20,
      IsChargingStation = false
    };
  }

  public static StateBicingDto CreateStateBicingDto()
  {
    return new StateBicingDto
    {
      BicingId = 1,
      NumBikesAvailable = 10,
      NumBikesAvailableMechanical = 6,
      NumBikesAvailableEbike = 4,
      NumDocksAvailable = 5,
      LastReported = DateTime.UtcNow,
      Status = "IN_SERVICE"
    };
  }
}
