using System;
using Entity;

public class RouteUserEntity
{
    public Guid RutaId { get; set; }
    public RouteEntity Ruta { get; set; }

    public Guid UsuarioId { get; set; }
    public UserEntity Usuario { get; set; }
}
