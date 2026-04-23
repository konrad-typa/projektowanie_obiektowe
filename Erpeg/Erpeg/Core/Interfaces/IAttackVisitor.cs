using Erpeg.Data.Models.Characters;

namespace Erpeg.Core.Interfaces;

public interface IAttackVisitor
{
    int VisitHeavyWeaponDamage(PlayerData player);
    int VisitLightWeaponDamage(PlayerData player);
    int VisitMagicWeaponDamage(PlayerData player);
    int VisitItemDamage(PlayerData player);
    
    int VisitHeavyWeaponDefense(PlayerData player);
    int VisitLightWeaponDefense(PlayerData player);
    int VisitMagicWeaponDefense(PlayerData player);
    int VisitItemDefense(PlayerData player);
}