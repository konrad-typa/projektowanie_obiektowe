using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Data.Models.Items;

public abstract class ConsumableItem : Item
{
    public int RestoreAmount { get; protected set; }

    protected ConsumableItem(string name, int value, int restoreAmount, double weight, char symbol = '?') 
        : base(name, value, weight, symbol)
    {
        RestoreAmount = restoreAmount;
    }

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

public class HealthPotion : ConsumableItem
{
    public HealthPotion(int restoreAmount, int value) 
        : base("Health Potion", value, restoreAmount, 2, '+') { }

    public override void Use(PlayerData player)
    {
        player.Heal(RestoreAmount);
        player.RemoveItemFromInventory(this);
        MessageLogSystem.Log($"You drank a {Name}. Restored {RestoreAmount} HP.");
    }
}

public class ManaPotion : ConsumableItem
{
    public ManaPotion(int restoreAmount, int value) 
        : base("Mana Potion", value, restoreAmount, 2, '+') { }

    public override void Use(PlayerData player)
    {
        player.RestoreMana(RestoreAmount);
        player.RemoveItemFromInventory(this);
        MessageLogSystem.Log($"You drank a {Name}. Restored {RestoreAmount} Mana.");
    }
}