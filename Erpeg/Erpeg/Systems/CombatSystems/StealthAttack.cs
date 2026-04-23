using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Systems.CombatSystems;

public class StealthAttack : IAttackVisitor
{
    public int VisitHeavyWeaponDamage(PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Strength) + player.GetTotalAttribute(AttributesType.Aggression);
        return (player.Damage+ stats) / 2;
    }

    public int VisitLightWeaponDamage(PlayerData player)
    {
        int stats = player.GetTotalAttribute(AttributesType.Dexterity) + player.GetTotalAttribute(AttributesType.Luck);
        return (player.Damage+ stats) * 2;
    }

    public int VisitMagicWeaponDamage(PlayerData player) => 1;

    public int VisitItemDamage(PlayerData player) => 0;

    public int VisitHeavyWeaponDefense(PlayerData player)
    {
        return player.Defense + player.GetTotalAttribute(AttributesType.Strength);
    }

    public int VisitLightWeaponDefense(PlayerData player)
    {
        return player.Defense + player.GetTotalAttribute(AttributesType.Dexterity);
    }

    public int VisitMagicWeaponDefense(PlayerData player) => player.Defense;
    public int VisitItemDefense(PlayerData player) => player.Defense;
}