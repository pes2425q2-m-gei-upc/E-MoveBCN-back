using System;
using Dto.User;
using Dto.Route;
namespace Dto.Chat;

public class ChatResponseDto
{
  public Guid Id { get; set; }
  public Guid RutaId { get; set; }
  public Guid HostId { get; set; }
  public Guid JoinerId { get; set; }
  
  public UserDto User2 { get; set; }        
  public PublishedRouteDto Route { get; set; }  
}
