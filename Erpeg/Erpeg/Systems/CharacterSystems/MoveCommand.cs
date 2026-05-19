using Erpeg.Core.Interfaces;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.GameStates;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.CharacterSystems;

public class MoveCommand(MapData map, PlayerSession session, int dx, int dy)
    : ICommand
{
    public void Execute()
    {
        var player = session.Player;
        var logger = session.Logger;
        
        int newX = player.Position.x + dx;
        int newY = player.Position.y + dy;
        var targetPos = (newX, newY);

        if (!(newX >= 0 && newX < map.SizeX && newY >= 0 && newY < map.SizeY &&
              map.Layout[newX, newY] != TileType.Wall))
        {
            logger.Log("Trying to sniff a wall, huh?");
        }
        else
        {
            if (map.Characters.TryGetValue((newX, newY), out var characterOnTile))
            {
                var callback = new InteractionCallback
                {
                    OnCombatStart = enemy => session.ChangeState(new CombatState(map, session, enemy))
                };
                characterOnTile.Interact(player, map, callback);
            }
            else
            {
                map.Characters.Remove(player.Position);
                player.Position = targetPos;
                map.Characters[targetPos] = player;
            }
        }
    }
}