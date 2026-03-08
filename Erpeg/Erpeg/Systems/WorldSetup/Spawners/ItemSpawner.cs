using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners;

public static class ItemSpawner
{
    public static void SpawnItems(MapData map)
    {
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetOneHandSword());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetBow());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetHealthPotion());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetWood());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetDust());
    }
}