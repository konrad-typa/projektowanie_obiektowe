namespace Erpeg.Data.Models.Items;

public class ConsumableData (string name, ItemType type, int value, int amount) 
    : ItemData (name, type, value)
{
    public int RestoreAmount { get; set; } = amount;
}