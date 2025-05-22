using Dto.Route;
using Dto.User;

namespace TestUtils;

    public static class TestRouteFactory
{
  private static readonly double[] _item = [2.15899, 41.38879];
  private static readonly double[] _itemArray = [2.1700, 41.4000];

  public static RouteDto CreateValidRouteDto(string? userId = null)
    {
        return new RouteDto
        {
        RouteId = Guid.NewGuid().ToString(),
        OriginLat = 41.38879,
        OriginLng = 2.15899,
        DestinationLat = 41.4000,
        DestionationLng = 2.1700,
        mean = "bike",
        Preference = "shortest",
        Distance = 1200.0,
        Duration = 600.0,
        Geometry = [_item, _itemArray],
        Instructions =
                    [
                        new RouteInstructionDto
                        {
                            Instruction = "Start at Plaça Catalunya",
                            Distance = 300.0,
                            Mode = "bike"
                        },
                        new RouteInstructionDto
                        {
                            Instruction = "Continue along Carrer d'Aragó",
                            Distance = 900.0,
                            Mode = "bike"
                        }
                    ],
        OriginStreetName = "Plaça Catalunya",
        DestinationStreetName = "Carrer d'Aragó",
        UserId = userId ?? Guid.NewGuid().ToString(),
        User = new UserDto
        {
            UserId = userId ?? Guid.NewGuid().ToString(),
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = "hashedpassword"
        }
        };
    }
}

