using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items.Weapons;

public class WeaponItem(string name, int value, 
    WeaponGripType grip, int damage, double hitspeed, int range, double weight, char symbol = '?')
    : Item(name, value, weight, symbol)
{
    public WeaponGripType Grip { get; } = grip;
    public override int Damage { get; protected set; } = damage;
    public double Hitspeed { get; } = hitspeed;
    public int Range { get; } = range;
    public override bool BlocksOffHand => Grip == WeaponGripType.TwoHanded;
    
    public override void OnPickedUp(PlayerData player, MapData map)
    {
        if (player.TryAddWeight(this.Weight))
        {
            player.Inventory.Add(this);
            map.Items.Remove(player.Position);
            GameLogger.Instance.Log($"Picked up {Name}.");
        }
    }
    
    public override void Use(PlayerData player)
    {
        player.EquipWeapon(this); 
        GameLogger.Instance.Log($"Equipped {Name}.");
    }
}