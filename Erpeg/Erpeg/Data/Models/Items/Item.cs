using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Data.Models.Items;

public abstract class Item(string name, int value, double weight, char symbol)
{
    public string Name { get; protected set; } = name;
    public int Value { get; protected set; } = value;
    public double Weight { get; protected set; } = weight;
    public char MapSymbol { get; protected set; } = symbol;
    public (int x, int y) Position { get; set; }
    public virtual bool BlocksOffHand => false;

    public abstract void OnPickedUp(PlayerData player, MapData map);
    
    public virtual void Use(PlayerData player)
    {
        MessageLogSystem.Log($"You cannot use {Name}.");
    }
}