using System.Security.Cryptography;
using Erpeg.Data.World.Models;

namespace Erpeg.Data.World.Maps;

public class MazeMap
{
    private static readonly Random Random = new Random();
    public static MapData GetDefinition()
    {
        var map = new MapData("Maze", 20, 40);
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