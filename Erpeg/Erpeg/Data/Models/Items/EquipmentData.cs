namespace Erpeg.Data.Models.Items;

// attr do konstruktora
public class EquipmentData(string name, ItemType type, int value, 
    EquipmentSlotType slotType, int defense, double weight, char symbol = '?')
    : ItemData(name, type, value, weight, symbol)
{
    public EquipmentSlotType SlotType { get; set; } = slotType;
    public int Defense { get; set; } = defense;
    
    public Dictionary<AttributesType, int> Attributes { get; set; } = new Dictionary<AttributesType, int>();
}