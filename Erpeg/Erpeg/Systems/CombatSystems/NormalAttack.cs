using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Systems.CombatSystems;

public class NormalAttack : IAttackVisitor
{
    public int VisitHeavyWeaponDamage(PlayerData player)
    {
        var stats =  player.GetTotalAttribute(AttributesType.Strength) + player.GetTotalAttribute(AttributesType.Aggression);
        return player.Damage +  stats;
    }

    public int VisitLightWeaponDamage(PlayerData player)
    {
        var stats =  player.GetTotalAttribute(AttributesType.Dexterity) + player.GetTotalAttribute(AttributesType.Luck);
        return player.Damage +  stats;
    }

    public int VisitMagicWeaponDamage(PlayerData player) => 1;

    public int VisitItemDamage(PlayerData player) => 0;

    public int VisitHeavyWeaponDefense(PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Strength) + player.GetTotalAttribute(AttributesType.Luck);
        return player.Defense + stats;
    }
    
    public int VisitLightWeaponDefense(PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Dexterity) + player.GetTotalAttribute(AttributesType.Luck);
        return player.Defense + stats;
    }

    public int VisitMagicWeaponDefense(PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Dexterity) + player.GetTotalAttribute(AttributesType.Luck);
        return player.Defense + stats;
    }

    public int VisitItemDefense(PlayerData player)
    {
        return player.Defense + player.GetTotalAttribute(AttributesType.Dexterity);
    }
}