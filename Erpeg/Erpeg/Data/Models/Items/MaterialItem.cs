using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Data.Models.Items;

public class MaterialItem : Item
{
    public MaterialItem(string name, int value, double weight, char symbol) 
        : base(name, value, weight, symbol) { }

    public override void OnPickedUp(PlayerData player, MapData map)
    {
        if (player.TryAddWeight(this.Weight))
        {
            player.Inventory.Add(this);
            map.Items.Remove(player.Position);
            MessageLogSystem.Log($"Picked up {Name}.");
        }
        else
        {
            MessageLogSystem.Log("Not enough space in inventory!");
        }
    }
}