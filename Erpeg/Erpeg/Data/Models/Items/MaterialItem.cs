using Erpeg.Core.Interfaces;
using Erpeg.Data.Events;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.EventSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items;

public class MaterialItem(string name, int value, double weight, char symbol, string  color = "")
    : Item(name, value, weight, symbol, color)
{
    public override void OnPickedUp(PlayerData player, MapData map, ILogger logger)
    {
        if (player.TryAddWeight(Weight))
        {
            player.Inventory.Add(this);
            map.Items.Remove(player.Position);
            logger.Log($"Picked up {Name}.");
        }
        else
        {
            logger.Log("Not enough space in inventory!");
        }
    }
}