using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.CharacterSystems;

public class MoveCommand : ICommand
{
    private readonly MapData _map;
    private readonly PlayerData _player;
    private readonly int _dx;
    private readonly int _dy;

    public MoveCommand(MapData map, PlayerData player, int dx, int dy)
    {
        _map = map;
        _player = player;
        _dx = dx;
        _dy = dy;
    }

    public void Execute()
    {
        int newX = _player.Position.x + _dx;
        int newY = _player.Position.y + _dy;

        if (newX >= 0 && newX < _map.SizeX && 
            newY >= 0 && newY < _map.SizeY && 
            _map.Layout[newX, newY] != TileType.Wall)
        {
            _player.Position = (newX, newY); 
        }
    }
}