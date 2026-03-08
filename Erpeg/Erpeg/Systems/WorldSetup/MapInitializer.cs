using Erpeg.Data.Content.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup;

public static class MapInitializer
{
    public static void SetupMap(MapData map)
    {
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetOneHandSword());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetTwoHandSword());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetOneHandSword());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetWood());
        ItemPlacementSystem.PlaceItem(map, ItemLibrary.GetDust());
    }
}