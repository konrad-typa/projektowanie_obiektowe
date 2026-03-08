using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup;

public class ItemPlacementSystem
{
    public static void PlaceItem(MapData map, ItemData item)
    {
        var pos = map.GetRandomEmptyTile();
        map.Items[pos] = item;
    }
}