using Erpeg.Data.Models.Characters;

namespace Erpeg.Core.Interfaces;

public interface IAttackVisitor
{
    int VisitHeavyWeaponDamage(int weaponDamage, PlayerData player);
    int VisitLightWeaponDamage(int weaponDamage, PlayerData player);
    int VisitMagicWeaponDamage(int weaponDamage, PlayerData player);
    int VisitItemDamage(int weaponDamage, PlayerData player);
    
    int VisitHeavyWeaponDefense(int weaponDefense, PlayerData player);
    int VisitLightWeaponDefense(int weaponDefense, PlayerData player);
    int VisitMagicWeaponDefense(int weaponDefense, PlayerData player);
    int VisitItemDefense(int itemDefense, PlayerData player);
}