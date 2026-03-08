using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.WorldSetup;

public class MazeGenerator
{
    private static readonly Random Random = new();

    public static MapData Generate()
    {
        var map = new MapData("Maze", 40, 20);

        for (int y = 0; y < map.SizeY; y++)
        {
            for (int x = 0; x < map.SizeX; x++)
            {
                map.Layout[x, y] =
                    Random.Next(0, 10) > 7 ? TileType.Wall : TileType.Empty;
            }
        }

        return map;
    }
}