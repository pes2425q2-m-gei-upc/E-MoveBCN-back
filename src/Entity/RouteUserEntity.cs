using System;
using Entity;

public class RouteUserEntity
{
  public required Guid RutaId { get; set; }
  public RouteEntity Ruta { get; set; }

  public required Guid UsuarioId { get; set; }
  public UserEntity Usuario { get; set; }
}
