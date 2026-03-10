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
    public Dictionary<(int x, int y), ItemData> Items { get; } = new();
    public List<CharacterData> Characters { get; } = new();
    
    // dodaj empty files, getnextemptyfile

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
    
    public ItemData? GetItemAt((int x, int y) position)
    {
        return Items.TryGetValue(position, out var item) ? item : null;
    }
}
