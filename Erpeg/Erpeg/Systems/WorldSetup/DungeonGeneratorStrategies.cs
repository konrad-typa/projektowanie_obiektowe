using Erpeg.Core.Interfaces;
using Erpeg.Data.Content.Enemies;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup;

public class ClassicDungeon : IDungeonGenStrategy
{
    public string StartMessage => "Welcome to the classic Dungeon!";
    public MapData Generate()
    {
        return DungeonBuilder.CreateFilled("Classic Dungeon", 40, 20)
            .AddCentralHall(16, 7)
            .AddRooms(20)
            .AddCorridors()
            .AddItems(10)
            .AddWeapons(5)
            .AddEq(5)
            .AddThemeEnemies(5, 
                EnemyLibrary.GetGoblin,
                EnemyLibrary.GetOrc)
            .Build();
    }
}