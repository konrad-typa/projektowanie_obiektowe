using Erpeg.Data.Events;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.EventSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items;

public class MaterialItem(string name, int value, double weight, char symbol, string  color = "")
    : Item(name, value, weight, symbol, color)
{
    public override void OnPickedUp(PlayerData player, MapData map)
    {
        if (player.TryAddWeight(Weight))
        {
            player.Inventory.Add(this);
            map.Items.Remove(player.Position);
            GameLogger.Instance.Log($"Picked up {Name}.");
        }
        else
        {
            GameLogger.Instance.Log("Not enough space in inventory!");
        }
    }
}