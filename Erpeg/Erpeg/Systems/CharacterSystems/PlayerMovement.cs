using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;

namespace Erpeg.Systems.CharacterSystems;

public class PlayerMovement
{
    public static event Action<(int, int)> OnPlayerMoved;

    public static void Initialize(MapData map, PlayerData player)
    {
        InputService.OnInput += (action) => HandleMovement(map, player, action);
    }
    
    public static void HandleMovement(MapData map, PlayerData player, InputActionType actionType)
    {
        (int dx, int dy) = actionType switch
        {
            InputActionType.MoveUp => (0, -1),
            InputActionType.MoveLeft => (-1, 0),
            InputActionType.MoveRight => (1, 0),
            InputActionType.MoveDown => (0, 1),
            _ => (0, 0)
        };
        
        int newX = player.Position.x + dx;
        int newY = player.Position.y + dy;

        if (newX >= 0 && newX < map.SizeX && 
            newY >= 0 && newY < map.SizeY && 
            map.Layout[newX, newY] != TileType.Wall)
        {
            player.Position = (newX, newY); 
            OnPlayerMoved?.Invoke(player.Position);
        }
    } 
}