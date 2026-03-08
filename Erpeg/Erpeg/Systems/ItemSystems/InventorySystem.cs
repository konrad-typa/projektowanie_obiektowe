using System.Runtime.ExceptionServices;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems.CharacterSystems;

namespace Erpeg.Systems.ItemSystems;

public class InventorySystem
{
    public static void Subscribe(MapData map, PlayerData player)
    {
        InputService.OnInput += (action) =>
        {
            if (action == InputActionType.PickUp)
                TryPickUp(map, player);
        };
    }

    private static void TryPickUp(MapData map, PlayerData player)
    {
        if (map.Items.TryGetValue(player.Position, out ItemData item))
        {
            if (player.CurrentWeight + item.Weight <= player.MaxWeight)
            {
                player.Inventory.Add(item);
                player.CurrentWeight += item.Weight;
                map.Items.Remove(player.Position);
            }
            else
            {
                // za ciężkie 
            }
        }
    }
}