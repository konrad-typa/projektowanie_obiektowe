namespace Erpeg.Data.Models.Items;

public enum RestoringType
{
    Health,
    Mana
}

public class ConsumableData (string name, ItemType type, int value, 
    RestoringType restoringType, int restore_amount, double weight, char symbol = '?') 
    : ItemData (name, type, value, weight, symbol)
{
    public RestoringType RestoringWhat = restoringType;
    public int RestoreAmount { get; set; } = restore_amount;
}