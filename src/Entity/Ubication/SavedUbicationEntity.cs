#nullable enable
namespace Entity.Ubication;
public class SavedUbicationEntity
{
  public required int UbicationId { get; set; }
  public required string UserEmail { get; set; }
  public required string StationType { get; set; }
  public required double Latitude { get; set; }
  public required double Longitude { get; set; }
  public int? Valoration { get; set; }
  public string? Comment { get; set; }

}
