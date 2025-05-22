using System.Text.Json.Serialization;

namespace Dto.Air;
public class AirSensorDto
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("latitud")]
  public double Latitud { get; set; }

  [JsonPropertyName("longitud")]
  public double Longitud { get; set; }

  [JsonPropertyName("index_qualitat_aire")]
  public double IndexQualitatAire { get; set; }

  [JsonPropertyName("nom_estacio")]
  public string NomEstacio { get; set; } = string.Empty;

  [JsonPropertyName("descripcio")]
  public string Descripcio { get; set; } = string.Empty;
}
