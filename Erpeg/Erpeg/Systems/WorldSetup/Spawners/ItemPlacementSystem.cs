using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup.Spawners;

public class ItemPlacementSystem
{
    public static void PlaceItem(MapData map, ItemData item)
    {
        var pos = map.GetRandomEmptyTile();
        int att = 0;
        while (map.Items.ContainsKey(pos) && att < map.SizeY * map.SizeX)
        {
            pos = map.GetRandomEmptyTile();
            att++;
        }
        map.Items[pos] = item;
    }
}