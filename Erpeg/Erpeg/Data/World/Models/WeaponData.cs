namespace Erpeg.Data.World.Models;

public class WeaponData(string name, ItemType type, int value, WeaponGripType grip, int damage, double hitspeed, int range)
    : ItemData(name, type, value)
{
    public WeaponGripType Grip { get; set; } = grip;
    public int Damage { get; set; } = damage;
    public double Hitspeed { get; set; } = hitspeed;
    public int Range { get; set; } = range;
    // public StatModifiers Stats {get; set;}
}