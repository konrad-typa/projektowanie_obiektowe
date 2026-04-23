using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items;

public abstract class Item(string name, int value, double weight, char symbol)
{
    public virtual string Name { get; protected set; } = name;
    public int Value { get; protected set; } = value;
    public double Weight { get; protected set; } = weight;
    public char MapSymbol { get; protected set; } = symbol;
    public (int x, int y) Position { get; set; }
    public virtual bool BlocksOffHand => false;
    public virtual Dictionary<AttributesType, int> Attributes { get; protected set; } = new()
    {
        { AttributesType.Strength, 0 },
        { AttributesType.Stamina, 0 },
        { AttributesType.Dexterity, 0 },
        { AttributesType.Intelligence, 0 },
        { AttributesType.Luck, 0 },
        { AttributesType.Aggression, 0 }
    };
    public virtual int Damage { get; protected set; } = 0;
    public virtual int Defense { get; protected set; } = 0;
    
    public virtual int AcceptDamage(IAttackVisitor visitor, PlayerData player) 
        => visitor.VisitItemDamage(player);

    public virtual int AcceptDefense(IAttackVisitor visitor, PlayerData player) 
        => visitor.VisitItemDefense(player);

    public abstract void OnPickedUp(PlayerData player, MapData map);
    
    public virtual void Use(PlayerData player)
    {
        GameLogger.Instance.Log($"You cannot use {Name}.");
    }
}