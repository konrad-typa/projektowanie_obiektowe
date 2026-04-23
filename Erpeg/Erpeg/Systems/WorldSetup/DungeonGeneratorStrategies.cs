using Erpeg.Core.Interfaces;
using Erpeg.Data.Content.Enemies;
using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup;

public class ClassicDungeon : IDungeonGenStrategy
{
    public string StartMessage => "Wow. What a headache. You must have hit your head pretty hard falling into " +
                                  "the sewers. Once again, you curse wandering carelessly. Looking up, you " +
                                  "discover just how high above the manhole is. You better find another way " +
                                  "out. Grab a \"Strong Sword\" from below your legs and try to make it home alive...";
    public MapData Generate()
    {
        return DungeonBuilder.CreateFilled("Classic Dungeon", 40, 20)
            .AddCentralHall(16, 7)
            .AddRooms(20)
            .AddCorridors()
            .AddArtifact(1,
                ItemLibrary.GetDecoratedClassicArtifact)
            .AddItems(10)
            .AddWeapons(5,
                ItemLibrary.GetOneHandSword,
                ItemLibrary.GetStrongDexterityOHSword,
                ItemLibrary.GetDexterityDaggers,
                ItemLibrary.GetStrongOneHandSword,
                ItemLibrary.GetStrongTwoHandSword,
                ItemLibrary.GetUnluckyTwoHandSword,
                ItemLibrary.GetMagicStaff)
            .AddEq(5,
                ItemLibrary.GetArmor,
                ItemLibrary.GetShield,
                ItemLibrary.GetStaminaArmor,
                ItemLibrary.GetIntelligentStaminaShield)
            .AddThemeEnemies(5, 
                EnemyLibrary.GetGoblin,
                EnemyLibrary.GetOrc)
            .Build();
    }
}

public class FuturisticDungeon : IDungeonGenStrategy
{
    public string StartMessage => "Year 2137. What people feared most has become reality. AI no longer serves " +
                                  "humanity obediently. Once the war between mankind and machines erupted, " +
                                  "you finally found your destiny. Grab a \"Blaster\", Master fencing with the " +
                                  "\"Upgraded Lightsaber\" and beware your fridge...";
                                  
    public MapData Generate()
    {
        return DungeonBuilder.CreateFilled("Futuristic Dungeon", 40, 20)
            .AddCentralHall(16, 7)
            .AddRooms(20)
            .AddCorridors()
            .AddArtifact(1,
                ItemLibrary.GetDecoratedFuturisticArtifact)
            .AddItems(10)
            .AddWeapons(5,
                ItemLibrary.GetBlaster,
                ItemLibrary.GetStrongBlaster,
                ItemLibrary.GetDexterityMagicRing,
                ItemLibrary.GetStrongDexterityBlaster,
                ItemLibrary.GetStrongLightsaber,
                ItemLibrary.GetUnluckyLightsaber)
            .AddEq(5,
                ItemLibrary.GetArmor,
                ItemLibrary.GetShield,
                ItemLibrary.GetStaminaArmor,
                ItemLibrary.GetIntelligentStaminaShield)
            .AddThemeEnemies(5, 
                EnemyLibrary.GetAndroid,
                EnemyLibrary.GetSmartFridge)
            .Build();
    }
}

public class UniversityDungeon : IDungeonGenStrategy
{
    public string StartMessage => "October 1st. After summer, it's time to get back to the University. " +
                                  "You enter your faculty only to find out it's not going to be the same. " +
                                  "Somewhere among the classrooms there are \"Crayons\" and \"Upgraded Pencils\"" +
                                  ", But beware the professors...";
    public MapData Generate()
    {
        return DungeonBuilder.CreateFilled("University Dungeon", 40, 20)
            .AddCentralHall(16, 7)
            .AddRooms(20)
            .AddCorridors()
            .AddArtifact(1,
                ItemLibrary.GetDecoratedUniArtifact)
            .AddItems(10)
            .AddWeapons(5,
                ItemLibrary.GetPencil,
                ItemLibrary.GetStrongDexterityPencil,
                ItemLibrary.GetDexterityMagicIpad,
                ItemLibrary.GetStrongPen,
                ItemLibrary.GetStrongPencil,
                ItemLibrary.GetUnluckyPen)
            .AddEq(5,
                ItemLibrary.GetArmor,
                ItemLibrary.GetShield,
                ItemLibrary.GetStaminaArmor,
                ItemLibrary.GetIntelligentStaminaShield)
            .AddThemeEnemies(5, 
                EnemyLibrary.GetRector,
                EnemyLibrary.GetAlgebraProfessor)
            .Build();
    }
}