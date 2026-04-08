using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Data.Models.Items;

public class EquipmentItem(string name, int value, 
    EquipmentSlotType slotType, int defense, double weight, char symbol = '?')
    : Item(name, value, weight, symbol)
{
    public EquipmentSlotType SlotType { get; set; } = slotType;
    public override int Defense { get; protected set; } = defense;
    
    public override void OnPickedUp(PlayerData player, MapData map)
    {
        if (player.TryAddWeight(this.Weight))
        {
            player.Inventory.Add(this);
            map.Items.Remove(player.Position);
            Systems.MessageLogSystem.Log($"Picked up {Name}.");
        }
    }
    
    public override void Use(PlayerData player)
    {
        player.EquipEq(this); 
        Systems.MessageLogSystem.Log($"Equipped {Name}.");
    }
}