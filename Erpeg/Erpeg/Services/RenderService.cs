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
            if (y < lHudlines.Count)
                sb.Append($"{lHudlines[y]}  "); // 2 spacje odstępu
            else 
                sb.Append(' ',  lUiWidth + 2); // 2 spacje odstępu
            
            for (int x = 0; x < map.SizeX; x++)
            {
                char symbol = map.Layout[x, y] switch
                {
                    TileType.Wall => '█',
                    TileType.Empty => ' ',
                    _ => '.'
                };

                if (map.Items.TryGetValue((x, y), out var item))
                    symbol = item.MapSymbol;

                if (map.Characters.TryGetValue((x, y), out var character))
                    symbol = character.MapSymbol;
                
                sb.Append(symbol);
            }

            if (y < rHudlines.Count)
                sb.Append($"  {rHudlines[y]}"); // 2 spacje odstępu
            else 
                sb.Append(' ',  rUiWidth + 2); // 2 spacje odstępu
            
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
