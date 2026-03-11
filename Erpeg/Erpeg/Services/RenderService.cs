using System.Text;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using System.Linq;

namespace Erpeg.Services;

public class RenderService : IService
{
    public void Initialize() { }

    public static string RenderFrame(MapData map, PlayerData player)
    {
        var sb = new StringBuilder();
        var hudlines = UIService.GenerateUILines(map, player);
        int uiWidth = hudlines.Count > 0 ? hudlines[0].Length : UIService.Width;
        
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
                
                var character = map.Characters.FirstOrDefault(c => c.Position == (x, y));
                if (character != null) 
                    symbol = character.MapSymbol;

                sb.Append(symbol);
            }
            string hudline = (y < hudlines.Count) ? hudlines[y] : new string(' ', uiWidth);
            sb.Append("    " + hudline);
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
