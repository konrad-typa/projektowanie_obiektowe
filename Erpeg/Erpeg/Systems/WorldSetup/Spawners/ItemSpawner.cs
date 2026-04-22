using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Items.Weapons;
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
    
    public static void SpawnThemeEq(MapData map, int count, params Func<EquipmentItem>[] eqGens)
    {
        for (int i = 0; i < count; i++)
        {
            var generator = eqGens[Random.Next(eqGens.Length)];
            var eq = generator();
            
            ItemPlacementSystem.PlaceItem(map, eq);
        }
    }
    
    public static void SpawnThemeWeapons(MapData map, int count, params Func<WeaponItem>[] weaponGens)
    {
        for (int i = 0; i < count; i++)
        {
            var generator = weaponGens[Random.Next(weaponGens.Length)];
            var weapon = generator();
            
            ItemPlacementSystem.PlaceItem(map, weapon);
        }
    }
}