using System.Data;
using System.Text;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Services.RenderServices;

public static class CenterUI
{
    public const int Width = 48;

    public static List<string> Render(MapData map, IGameState gameState)
    {
        var allLines = new List<string>();

        // info
        string infoText = GameLogger.Instance.GetContext();
        var infoContent = new List<string> { UIHelper.CenterAnsi(infoText, Width - 2) };
        allLines.AddRange(UIHelper.DrawBox("Info", infoContent, Width, 1, 
            UIHelper.MutedCobalt));

        // mapa
        var mapContent = new List<string>();
        for (int y = 0; y < map.SizeY; y++)
        {
            var rowSb = new StringBuilder();

            int padding = (Width - 2 - map.SizeX) / 2;
            rowSb.Append(new string(' ', Math.Max(0, padding)));

            for (int x = 0; x < map.SizeX; x++)
            {
                char symbol = map.Layout[x, y] switch
                {
                    TileType.Wall => '█',
                    TileType.Empty => ' ',
                    _ => '?'
                };
                string color = UIHelper.FrameStone;

                if (map.Items.TryGetValue((x, y), out var item))
                {
                    symbol = item.MapSymbol;
                    color = item.Color;
                }

                if (map.Characters.TryGetValue((x, y), out var character))
                {
                    symbol = character.MapSymbol;
                    color = character.Color;
                }

                rowSb.Append($"{color}{symbol}{UIHelper.ColorReset}");
            }
            mapContent.Add(rowSb.ToString());
        }
        allLines.AddRange(UIHelper.DrawBox(map.Name, mapContent, Width, 19, 
            UIHelper.MutedAmethyst));

        // dziennik
        var journalContent = new List<string>();
        var logs = gameState.GetLogHistory();
        foreach (var log in logs.TakeLast(5))
        {
            var time = log.Item1;
            var msg = log.Item2;
            var wrappedLines = UIHelper.WrapText($"" +
                                                 $"{UIHelper.ColorDarkGray}[{time:mm:ss}]{UIHelper.ColorReset} " +
                                                 $"{msg}", Width - 4);
            foreach (var line in wrappedLines)
            {
                journalContent.Add($"{line}");
            }
        }
        allLines.AddRange(UIHelper.DrawBox("Journal", journalContent, Width, 5, 
            UIHelper.MutedGold));

        return allLines;
    }
}