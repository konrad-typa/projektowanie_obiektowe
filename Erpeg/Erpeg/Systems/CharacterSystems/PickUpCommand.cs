using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Systems.CharacterSystems;

public class PickUpCommand : ICommand
{
    private readonly MapData _map;
    private readonly PlayerData _player;

    public PickUpCommand(MapData map, PlayerData player)
    {
        _map = map;
        _player = player;
    }

    public void Execute()
    {
        if (_map.Items.TryGetValue(_player.Position, out Item item))
        {
            item.OnPickedUp(_player, _map);
        }
        else
        {
            MessageLogSystem.Log("There is nothing to pick up here.");
        }
    }
}