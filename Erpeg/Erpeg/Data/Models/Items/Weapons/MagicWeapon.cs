using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Data.Models.Items.Weapons;

public class MagicWeapon(
    string name,
    int value,
    WeaponGripType grip,
    int damage,
    double hitspeed,
    int range,
    double weight,
    char symbol = '?')
    : WeaponItem(name, value, grip, damage, hitspeed, range, weight, symbol)
{
    public override int AcceptDamage(IAttackVisitor visitor, PlayerData player, int currentDamage) 
        => visitor.VisitMagicWeaponDamage(currentDamage, player);

    public override int AcceptDefense(IAttackVisitor visitor, PlayerData player, int currentDefense) 
        => visitor.VisitMagicWeaponDefense(currentDefense, player);
}