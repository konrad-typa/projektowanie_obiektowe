using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Systems.CombatSystems;

public class MagicAttack : IAttackVisitor
{ 
    public int VisitHeavyWeaponDamage(int weaponDamage, PlayerData player) => 1; // Ciężka: 1

    public int VisitLightWeaponDamage(int weaponDamage, PlayerData player) => 1; // Lekka: 1

    public int VisitMagicWeaponDamage(int weaponDamage, PlayerData player) => weaponDamage; // Bez zmian

    public int VisitItemDamage(int itemDamage, PlayerData player) => 0; // Nie-broń: 0

    public int VisitHeavyWeaponDefense(int weaponDefense, PlayerData player)
    {
        return weaponDefense + player.GetTotalAttribute(AttributesType.Luck);
    }

    public int VisitLightWeaponDefense(int weaponDefense, PlayerData player)
    {
        return weaponDefense + player.GetTotalAttribute(AttributesType.Luck);
    }

    public int VisitMagicWeaponDefense(int weaponDefense, PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Intelligence) * 2;
        return weaponDefense + stats;
    }

    public int VisitItemDefense(int itemDefense, PlayerData player)
    {
        return itemDefense + player.GetTotalAttribute(AttributesType.Luck);
    }
}