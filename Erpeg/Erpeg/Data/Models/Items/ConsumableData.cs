namespace Erpeg.Data.Models.Items;

public class ConsumableData (string name, ItemType type, int value, 
    int restore_amount, double weight, char symbol = '?') 
    : ItemData (name, type, value, weight, symbol)
{
    public int RestoreAmount { get; set; } = restore_amount;
}