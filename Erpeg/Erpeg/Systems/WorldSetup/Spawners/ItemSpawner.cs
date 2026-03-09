using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners;

public static class ItemSpawner
{
    public static void SpawnItems(MapData map)
    {
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetOneHandSword());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetTwoHandSword());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetBow());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetHealthPotion());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetManaPotion());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetWood());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetDust());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetArmor());
        
        // kasa
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetCoins(10));
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetCoins(20));
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetCoins(50));
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetGold(100));
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetGold(50));
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetGold(50));
    }
}