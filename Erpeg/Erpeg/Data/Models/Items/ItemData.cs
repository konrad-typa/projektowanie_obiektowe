namespace Erpeg.Data.Models.Items;

public class ItemData(string name, ItemType type, int value = 1, double weight = 0, char symbol = '?')
{
    public string Name = name;
    public ItemType Type = type;
    public int Value = value;
    public (int x, int y) Position { get; set; } = (0, 0);
    public char MapSymbol { get; set; } = symbol;
    public double Weight { get; set; } = weight;
}