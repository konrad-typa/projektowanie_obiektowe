using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.MovementSystem;

public class PlayerMovement
{
    public static void TryMovePlayer(MapData map, PlayerData player, ConsoleKey key)
    {
        var pos = player.Position;

        (int dx, int dy) = key switch
        {
            ConsoleKey.W => (0, -1),
            ConsoleKey.A => (-1, 0),
            ConsoleKey.D => (1, 0),
            ConsoleKey.S => (0, 1),
            _ => (0, 0)
        };
        
        int newX = player.Position.x + dx;
        int newY = player.Position.y + dy;

        if (newX >= 0 && newX < map.SizeX && 
            newY >= 0 && newY < map.SizeY && 
            map.Layout[newX, newY] != TileType.Wall)
        {
            player.Position = (newX, newY);
        }
    } 
}