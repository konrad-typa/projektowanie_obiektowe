using Erpeg.Data.Models.Characters;
using Erpeg.Systems.EventSystems;

namespace Erpeg.Data.Content.Enemies;

public static class EnemyLibrary
{
    // classic theme
    public static EnemyData GetGoblin() => 
        new("Goblin", (0, 0), attack: 60, defense: 40, 
            "Aggressive", EventManager.SoundSystem, EventManager.SpeciesSystem, maxhp: 300, hp: 300);
    public static EnemyData GetOrc() => 
        new("Orc", (0, 0), attack: 200, defense: 70, 
            "Cowardly", EventManager.SoundSystem, EventManager.SpeciesSystem, maxhp: 500, hp: 500);
        
    // futuristic theme
    public static EnemyData GetAndroid() => 
        new("Android", (0, 0), attack: 60, defense: 40, 
            "Aggressive", EventManager.SoundSystem, EventManager.SpeciesSystem, maxhp: 300, hp: 300);
    public static EnemyData GetSmartFridge() => 
        new("SmartFridge", (0, 0), attack: 200, defense: 70, 
            "Cowardly", EventManager.SoundSystem, EventManager.SpeciesSystem, maxhp: 500, hp: 500);
    
    // university theme
    public static EnemyData GetAlgebraProfessor() =>
        new("Algebra Professor ", (0, 0), attack: 60, defense: 40, 
            "Aggressive", EventManager.SoundSystem, EventManager.SpeciesSystem, maxhp: 300, hp: 300);
    public static EnemyData GetRector() => 
        new("Rector", (0, 0), attack: 200, defense: 70,
            "Cowardly", EventManager.SoundSystem, EventManager.SpeciesSystem, maxhp: 500, hp: 500);
}