using System;
using Entity.Route;
using Entity.User;
namespace Entity.Chat;
public class ChatEntity
{
  public required Guid ChatId { get; set; }
  public required Guid RouteId { get; set; }
  public required Guid User1Id { get; set; }
  public required Guid User2Id { get; set; }
  public virtual UserEntity UserId1Navigation { get; set; }
  public virtual UserEntity UserId2Navigation { get; set; }
  public virtual PublishedRouteEntity PublicRouteNavigation { get; set; }
}
