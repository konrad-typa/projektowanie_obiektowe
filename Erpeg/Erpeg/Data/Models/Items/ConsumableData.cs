namespace Erpeg.Data.Models.Items;

public class ConsumableData (string name, ItemType type, int value, int amount, char symbol = '?') 
    : ItemData (name, type, value, symbol)
{
    public int RestoreAmount { get; set; } = amount;
}