namespace Erpeg.Data.World.Models;

public class MapData(string name, int sizeX, int sizeY)
{
    public string Name = name;
    public int SizeX = sizeX;
    public int SizeY = sizeY;
    public TileType[,] Layout = new TileType[sizeX, sizeY];
}
