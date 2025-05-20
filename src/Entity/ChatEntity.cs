using System;
using System.Collections.Generic;
using src.Entity.Route;
namespace Entity;


public class ChatEntity
{
  public required Guid ChatId { get; set; }
  public required Guid RouteId { get; set; }
  public required Guid User1Id{ get; set; }
  public required Guid User2Id { get; set; }

  public  virtual UserEntity UserId1Navigation { get; set; }
  public virtual  UserEntity UserId2Navigation { get; set; }

  public virtual PublishedRouteEntity PublicRouteNavigation { get; set; }
}