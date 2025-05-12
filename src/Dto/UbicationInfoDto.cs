namespace Dto;
public class UbicationInfoDto
{
  public required int UbicationId { get; set; }
  public required string UserEmail { get; set; }
  public required string StationType { get; set; }
  public int? Valoration { get; set; }
  public string? Comment { get; set; }
}
