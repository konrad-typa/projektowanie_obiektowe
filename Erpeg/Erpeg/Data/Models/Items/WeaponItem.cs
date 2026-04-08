using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Data.Models.Items;

public class WeaponItem(string name, int value, 
    WeaponGripType grip, int damage, double hitspeed, int range, double weight, char symbol = '?')
    : Item(name, value, weight, symbol)
{
    public WeaponGripType Grip { get; } = grip;
    public int Damage { get; } = damage;
    public double Hitspeed { get; } = hitspeed;
    public int Range { get; } = range;
    public override bool BlocksOffHand => Grip == WeaponGripType.TwoHanded;
   
    public Dictionary<AttributesType, int> Attributes { get; set; } = new Dictionary<AttributesType, int>();
    
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
        player.EquipWeapon(this); 
        Systems.MessageLogSystem.Log($"Equipped {Name}.");
    }
}