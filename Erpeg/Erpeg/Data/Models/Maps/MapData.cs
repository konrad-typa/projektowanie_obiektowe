using System.Runtime.CompilerServices;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;

namespace Erpeg.Data.Models.Maps;

public class MapData(string name, int sizeX, int sizeY)
{
    public string Name { get; } = name;
    public int SizeX { get; } = sizeX;
    public int SizeY { get; } = sizeY;
    public TileType[,] Layout { get; } = new TileType[sizeX, sizeY];
    public Dictionary<(int x, int y), Item> Items { get; } = new();
    public List<CharacterData> Characters { get; } = new();

    private static readonly Random Rng = new();
    public (int x, int y) GetRandomEmptyTile()
    {
        
        int x = Rng.Next(0, this.SizeX);
        int y = Rng.Next(0, this.SizeY);
        while (this.Layout[x, y] != TileType.Empty)
        {
            x = Rng.Next(0, this.SizeX); 
            y = Rng.Next(0, this.SizeY);
        }
        return (x, y);
    }
    
    public Item? GetItemAt((int x, int y) position)
    {
        return Items.TryGetValue(position, out var item) ? item : null;
    }
    
    public bool TryGetAvailableTile((int x, int y) center, out (int x, int y) freePos)
    {
        if (!Items.ContainsKey(center) && Layout[center.x, center.y] != TileType.Wall)
        {
            freePos = center;
            return true;
        }
        
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int dx = Math.Clamp(center.x + i, 0, SizeX - 1);
                int dy = Math.Clamp(center.y + j, 0, SizeY - 1);
                var checkPos = (dx, dy);

                if (!Items.ContainsKey(checkPos) && Layout[dx, dy] != TileType.Wall)
                {
                    freePos = checkPos;
                    return true;
                }
            }
        }

        freePos = center;
        return false;
    }
}
