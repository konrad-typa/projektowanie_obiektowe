namespace Erpeg.Data.Models.Items;

public class EquipmentData(string name, ItemType type, int value, EquipmentSlotType slotType, int defense)
    : ItemData(name, type, value)
{
    public EquipmentSlotType SlotType { get; set; } = slotType;
    public int Defense { get; set; } = defense;
    // public StatModifiers Stats {get; set;}
}