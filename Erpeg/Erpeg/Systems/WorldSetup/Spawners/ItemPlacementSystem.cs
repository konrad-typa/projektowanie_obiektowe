using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners;

public class ItemPlacementSystem
{
    public static void PlaceItem(MapData map, ItemData item)
    {
        var pos = map.GetRandomEmptyTile();
        while (map.Items.ContainsKey(pos))
            pos = map.GetRandomEmptyTile();
        map.Items[pos] = item;
    }
}