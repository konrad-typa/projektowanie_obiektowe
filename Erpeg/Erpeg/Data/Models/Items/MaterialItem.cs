using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items;

public class MaterialItem(string name, int value, double weight, char symbol)
    : Item(name, value, weight, symbol)
{
    public override void OnPickedUp(PlayerData player, MapData map)
    {
        if (player.TryAddWeight(this.Weight))
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