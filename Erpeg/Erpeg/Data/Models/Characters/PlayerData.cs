namespace Erpeg.Data.Models.Characters;

public class PlayerData(string name, (int x, int y) position, int maxhp = 500, int hp = 500, char symbol = '¶')
    : CharacterData(name, position, maxhp, hp, symbol)
{
    
}