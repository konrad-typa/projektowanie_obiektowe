using System.Text;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using System.Linq;
using Erpeg.Systems;

namespace Erpeg.Services;

public class RenderService : IService
{
    public void Initialize() { }

    public static string RenderFrame(MapData map, PlayerData player, IGameState gameState)
    {
        var sb = new StringBuilder();
        var rHudlines = UIService.GenerateUILinesRight(map, player);
        var topHudlines = UIService.GenerateUILinesTop(map, player);
        var lHudlines = UIService.GenerateUILinesLeft(map, player, gameState);
        var bottomHudlines = UIService.GenerateUILinesBottom(map, player);
        
        int rUiWidth = rHudlines.Count > 0 ? rHudlines[0].Length : UIService.Width;
        int lUiWidth = lHudlines.Count > 0 ? lHudlines[0].Length : UIService.Width;

        // top hud
        foreach (var topHudline in topHudlines)
            sb.AppendLine(topHudline);
        
        for (int y = 0; y < map.SizeY; y++)
        {
            string lHudline = (y < lHudlines.Count) ? lHudlines[y] : new string(' ', lUiWidth);
            sb.Append(lHudline + "  "); // 3 spacje odstępu
            
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

            string rHudline = (y < rHudlines.Count) ? rHudlines[y] : new string(' ', rUiWidth);
            sb.Append("  " + rHudline); // 3 spacje odstępu
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
