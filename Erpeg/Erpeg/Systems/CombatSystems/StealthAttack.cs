using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Systems.CombatSystems;

public class StealthAttack : IAttackVisitor
{
    public int VisitHeavyWeaponDamage(int weaponDamage, PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Strength) + player.GetTotalAttribute(AttributesType.Aggression);
        return (weaponDamage + stats) / 2;
    }

    public int VisitLightWeaponDamage(int weaponDamage, PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Dexterity) + player.GetTotalAttribute(AttributesType.Luck);
        return (weaponDamage + stats) * 2;
    }

    public int VisitMagicWeaponDamage(int weaponDamage, PlayerData player) => 1;

    public int VisitItemDamage(int itemDamage, PlayerData player) => 0;

    public int VisitHeavyWeaponDefense(int weaponDefense, PlayerData player)
    {
        return weaponDefense + player.GetTotalAttribute(AttributesType.Strength);
    }

    public int VisitLightWeaponDefense(int weaponDefense, PlayerData player)
    {
        return weaponDefense + player.GetTotalAttribute(AttributesType.Dexterity);
    }

    public int VisitMagicWeaponDefense(int weaponDefense, PlayerData player) => weaponDefense;
    public int VisitItemDefense(int itemDefense, PlayerData player) => itemDefense;
}