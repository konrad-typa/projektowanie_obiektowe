using System.Text;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Services;

public class RenderService : IService
{
    public void Initialize() { }

    public static string RenderFrame(MapData map)
    {
        var sb = new StringBuilder();
        
        for (int y = 0; y < map.SizeY; y++)
        {
            for (int x = 0; x < map.SizeX; x++)
            {
                char symbol = map.Layout[x, y] switch
                {
                    TileType.Wall => '█',
                    TileType.Empty => ' ',
                    _ => '.'
                };
                
                if (map.Items.ContainsKey((x, y)))
                    symbol = map.Items[(x, y)].MapSymbol;

                sb.Append(symbol);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
