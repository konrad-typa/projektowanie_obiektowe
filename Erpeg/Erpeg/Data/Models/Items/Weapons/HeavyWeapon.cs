using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Data.Models.Items.Weapons;

public class HeavyWeapon(
    string name,
    int value,
    WeaponGripType grip,
    int damage,
    double hitspeed,
    int range,
    double weight,
    char symbol = '?',
    string color = "")
    : WeaponItem(name, value, grip, damage, hitspeed, range, weight, symbol, color)
{
    public override int AcceptDamage(IAttackVisitor visitor, PlayerData player) 
        => visitor.VisitHeavyWeaponDamage(player);

    public override int AcceptDefense(IAttackVisitor visitor, PlayerData player) 
        => visitor.VisitHeavyWeaponDefense(player);

    public override int NoiseRange => 7;
}