using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.CharacterSystems;

public class MoveCommand(MapData map, PlayerData player, int dx, int dy)
    : ICommand
{
    public void Execute()
    {
        int newX = player.Position.x + dx;
        int newY = player.Position.y + dy;
        var targetPos = (newX, newY);

        if (newX >= 0 && newX < map.SizeX && newY >= 0 && newY < map.SizeY && map.Layout[newX, newY] != TileType.Wall)
        {
            var characterOnTile = map.Characters.FirstOrDefault(c => c.Position == targetPos);

            if (characterOnTile != null)
            {
                characterOnTile.Interact(player, map);
            }
            else
            {
                player.Position = targetPos;
            }
        }
    }
}