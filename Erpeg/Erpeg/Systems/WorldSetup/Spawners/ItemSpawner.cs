using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners;

public static class ItemSpawner
{
    private static readonly Random Random = new();
    
    public static void SpawnRandomItems(MapData map, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Item item = Random.Next(6) switch
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
            var item = Random.Next(6) switch
            {
                0 => ItemLibrary.GetStrongOneHandSword(),
                1 => ItemLibrary.GetMagicStaff(),
                2 => ItemLibrary.GetStrongTwoHandSword(),
                3 => ItemLibrary.GetUnluckyTwoHandSword(),
                4 => ItemLibrary.GetStrongDexterityOHSword(),
                5 => ItemLibrary.GetDexterityDaggers()
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
                0 => ItemLibrary.GetStaminaArmor(),
                1 => ItemLibrary.GetIntelligentArtifact(),
                2 => ItemLibrary.GetIntelligentStaminaShield() 
            };

            ItemPlacementSystem.PlaceItem(map, item);
        }
    }
}