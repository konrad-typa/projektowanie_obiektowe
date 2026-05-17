using Erpeg.Core.Interfaces;
using Erpeg.Data.Events;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.EventSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items;

public class EquipmentItem(string name, int value, 
    EquipmentSlotType slotType, int defense, double weight, char symbol = '?', string color = "")
    : Item(name, value, weight, symbol, color)
{
    public EquipmentSlotType SlotType { get; set; } = slotType;
    public override int Defense { get; protected set; } = defense;
    
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
    
    public override void Use(PlayerData player, ILogger logger)
    {
        player.EquipEq(this); 
        logger.Log($"Equipped {Name}.");
    }
}