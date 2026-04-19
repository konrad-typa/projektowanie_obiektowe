using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items;

public abstract class ConsumableItem(string name, int value, int restoreAmount, double weight, char symbol = '?')
    : Item(name, value, weight, symbol)
{
    protected int RestoreAmount { get; set; } = restoreAmount;

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

public class HealthPotion(int restoreAmount, int value) : ConsumableItem("Health Potion", value, restoreAmount, 2, '+')
{
    public override void Use(PlayerData player)
    {
        player.Heal(RestoreAmount);
        player.RemoveItemFromInventory(this);
        GameLogger.Instance.Log($"You drank a {Name}. Restored {RestoreAmount} HP.");
    }
}

public class ManaPotion(int restoreAmount, int value) : ConsumableItem("Mana Potion", value, restoreAmount, 2, '+')
{
    public override void Use(PlayerData player)
    {
        player.RestoreMana(RestoreAmount);
        player.RemoveItemFromInventory(this);
        GameLogger.Instance.Log($"You drank a {Name}. Restored {RestoreAmount} Mana.");
    }
}