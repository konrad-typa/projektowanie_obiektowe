using Erpeg.Data.Models.Maps;

namespace Erpeg.Data.Models.Characters;

public class CharacterData(string name, (int x, int y) position, 
    int maxhp = 500, int hp = 500, char symbol = '@')
{
    public string Name { get; set; } = name;
    public (int x, int y) Position { get; set; } = position;
    public char MapSymbol { get; } = symbol;

    private int _hp = hp;
    private int _maxHp = maxhp;
    public virtual int MaxHp
    {
        get => _maxHp;
        set
        {
            _maxHp = value;
            _hp = Math.Clamp(_hp, 0, _maxHp);
        }
    }
    public int Hp
    {
        get => _hp; 
        set => _hp = Math.Clamp(value, 0, MaxHp);
    }

    public virtual void Interact(PlayerData player, MapData map)
    {
    }
}