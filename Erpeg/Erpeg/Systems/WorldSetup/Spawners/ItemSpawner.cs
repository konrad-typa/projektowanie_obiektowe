using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners;

public static class ItemSpawner
{
    private static readonly Random Random = new();
    
    public static void SpawnRandomItems(MapData map, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var item = Random.Next(6) switch
            {
                0 => ItemLibrary.GetHealthPotion(),
                1 => ItemLibrary.GetWood(),
                2 => ItemLibrary.GetIron(),
                3 => ItemLibrary.GetDust(),
                4 => ItemLibrary.GetCoins(20),
                5 => ItemLibrary.GetGold(10)
            };

            ItemPlacementSystem.PlaceItem(map, item);
        }
    }
    
    public static void SpawnRandomWeapons(MapData map, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var item = Random.Next(4) switch
            {
                0 => ItemLibrary.GetOneHandSword(),
                1 => ItemLibrary.GetBow(),
                2 => ItemLibrary.GetTwoHandSword(),
                3 => ItemLibrary.GetDaggers()
            };

            ItemPlacementSystem.PlaceItem(map, item);
        }
    }
    
    public static void SpawnRandomEq(MapData map, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var item = Random.Next(3) switch
            {
                0 => ItemLibrary.GetArmor(),
                1 => ItemLibrary.GetArtifact(),
                2 => ItemLibrary.GetShield()
            };

            ItemPlacementSystem.PlaceItem(map, item);
        }
    }
}