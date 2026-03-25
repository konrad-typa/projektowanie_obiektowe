using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup;

public class ClassicDungeonGenerator : IDungeonGenStrategy
{
    public MapData Generate()
    {
        return DungeonBuilder.CreateFilled("Classic Dungeon", 40, 20)
            .AddCentralHall(16, 7)
            .AddRooms(20)
            .AddCorridors()
            .AddItems(10)
            .AddWeapons(5)
            .AddEq(5)
            .Build();
    }
}