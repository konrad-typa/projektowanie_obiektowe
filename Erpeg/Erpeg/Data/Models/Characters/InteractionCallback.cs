namespace Erpeg.Data.Models.Characters;

public class InteractionCallback
{
    public Action<EnemyData>? OnCombatStart { get; set; }
}