using Erpeg.Data.Models.Items;

namespace Erpeg.Data.Models.Characters;

public class PlayerData(string name, (int x, int y) position, int maxhp = 500, int hp = 500, char symbol = '¶')
    : CharacterData(name, position, maxhp, hp, symbol)
{
    public List<ItemData> Inventory { get; set; } = new List<ItemData>();
    public double CurrentWeight { get; set; } = 0;
    public double MaxWeight { get; set; } = 100;
}