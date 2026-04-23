using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items.Weapons;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Data.Models.Items.Decorators;

public abstract class WeaponDecorator : WeaponItem
{
    protected readonly WeaponItem _innerWeapon;

    protected WeaponDecorator(WeaponItem innerWeapon) 
        : base(innerWeapon.Name, innerWeapon.Value, innerWeapon.Grip, innerWeapon.Damage, innerWeapon.Hitspeed, innerWeapon.Range, innerWeapon.Weight, innerWeapon.MapSymbol)
    {
        _innerWeapon = innerWeapon;
    }
    
    public override string Name => _innerWeapon.Name;
    public override int Damage => _innerWeapon.Damage;
    public override Dictionary<AttributesType, int> Attributes => _innerWeapon.Attributes;
    public override bool BlocksOffHand => _innerWeapon.BlocksOffHand;
    
    public override int AcceptDamage(IAttackVisitor visitor, PlayerData player)
    {
        return _innerWeapon.AcceptDamage(visitor, player);
    }

    public override int AcceptDefense(IAttackVisitor visitor, PlayerData player)
    {
        return _innerWeapon.AcceptDefense(visitor, player);
    }
}

public class ExtraDmgWeaponDecorator(WeaponItem innerWeapon) : WeaponDecorator(innerWeapon)
{
    public override string Name => $"{_innerWeapon.Name} (+dmg)";
    
    public override int Damage => _innerWeapon.Damage + 5;
}

public class UnluckyWeaponDecorator(WeaponItem innerWeapon) : WeaponDecorator(innerWeapon)
{
    public override string Name => $"{_innerWeapon.Name} (-lck)";
    public override Dictionary<AttributesType, int> Attributes
    {
        get
        {
            var modifiedAttributes = new Dictionary<AttributesType, int>(_innerWeapon.Attributes);
            modifiedAttributes[AttributesType.Luck] -= 5;

            return modifiedAttributes;
        }
    }
}

public class StrongWeaponDecorator(WeaponItem innerWeapon) : WeaponDecorator(innerWeapon)
{
    public override string Name => $"{_innerWeapon.Name} (+str)";
    public override Dictionary<AttributesType, int> Attributes
    {
        get
        {
            var modifiedAttributes = new Dictionary<AttributesType, int>(_innerWeapon.Attributes);
            modifiedAttributes[AttributesType.Strength] += 5;

            return modifiedAttributes;
        }
    }
}

public class DexterityWeaponDecorator(WeaponItem innerWeapon) : WeaponDecorator(innerWeapon)
{
    public override string Name => $"{_innerWeapon.Name} (+dxt)";
    public override Dictionary<AttributesType, int> Attributes
    {
        get
        {
            var modifiedAttributes = new Dictionary<AttributesType, int>(_innerWeapon.Attributes);
            modifiedAttributes[AttributesType.Dexterity] += 5;

            return modifiedAttributes;
        }
    }
}

// dodaj nowe