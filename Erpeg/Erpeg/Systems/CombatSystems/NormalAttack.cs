using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Systems.CombatSystems;

public class NormalAttack : IAttackVisitor
{
    public int VisitHeavyWeaponDamage(int weaponDamage, PlayerData player) => weaponDamage;

    public int VisitLightWeaponDamage(int weaponDamage, PlayerData player) => weaponDamage;

    public int VisitMagicWeaponDamage(int weaponDamage, PlayerData player) => 1;

    public int VisitItemDamage(int itemDamage, PlayerData player) => 0;

    public int VisitHeavyWeaponDefense(int weaponDefense, PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Strength) + player.GetTotalAttribute(AttributesType.Luck);
        return weaponDefense + stats;
    }
    
    public int VisitLightWeaponDefense(int weaponDefense, PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Dexterity) + player.GetTotalAttribute(AttributesType.Luck);
        return weaponDefense + stats;
    }

    public int VisitMagicWeaponDefense(int weaponDefense, PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Dexterity) + player.GetTotalAttribute(AttributesType.Luck);
        return weaponDefense + stats;
    }

    public int VisitItemDefense(int itemDefense, PlayerData player)
    {
        return itemDefense + player.GetTotalAttribute(AttributesType.Dexterity);
    }
}