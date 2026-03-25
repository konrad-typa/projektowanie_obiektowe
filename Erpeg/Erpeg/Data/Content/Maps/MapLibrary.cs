using Erpeg.Data.Models.Maps;

namespace Erpeg.Data.Content.Maps;

public class MapLibrary
{
    public static MapData GetMazeMap() => 
        new MapData("Maze", 20, 40);
}