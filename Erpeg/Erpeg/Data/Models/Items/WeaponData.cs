namespace Erpeg.Data.Models.Items;

public class WeaponData(string name, ItemType type, int value, 
    WeaponGripType grip, int damage, double hitspeed, int range, double weight, char symbol = '?')
    : ItemData(name, type, value, weight, symbol)
{
    public WeaponGripType Grip { get; } = grip;
    public int Damage { get; } = damage;
    public double Hitspeed { get; } = hitspeed;
    public int Range { get; } = range;
   
    public Dictionary<AttributesType, int> Attributes { get; set; } = new Dictionary<AttributesType, int>();
}