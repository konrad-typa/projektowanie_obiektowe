using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Systems.CombatSystems;

public class MagicAttack : IAttackVisitor
{ 
    public int VisitHeavyWeaponDamage(PlayerData player) => 1;

    public int VisitLightWeaponDamage(PlayerData player) => 1;

    public int VisitMagicWeaponDamage(PlayerData player)
    {
        var stats = player.GetTotalAttribute(AttributesType.Intelligence) * 3;
        return player.Damage + stats; 
    } 

    public int VisitItemDamage(PlayerData player) => 0;

    public int VisitHeavyWeaponDefense(PlayerData player)
    {
        return player.Defense + player.GetTotalAttribute(AttributesType.Luck);
    }

    public int VisitLightWeaponDefense(PlayerData player)
    {
        return player.Defense + player.GetTotalAttribute(AttributesType.Luck);
    }

    public int VisitMagicWeaponDefense(PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Intelligence) * 2;
        return player.Defense + stats;
    }

    public int VisitItemDefense(PlayerData player)
    {
        return player.Defense + player.GetTotalAttribute(AttributesType.Luck);
    }
}