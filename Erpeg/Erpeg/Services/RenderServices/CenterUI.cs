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
        allLines.AddRange(UIHelper.DrawBox("Info", infoContent, Width, 1));

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
                    _ => '.'
                };

                if (map.Items.TryGetValue((x, y), out var item)) symbol = item.MapSymbol;
                if (map.Characters.TryGetValue((x, y), out var character)) symbol = character.MapSymbol;
                
                if (symbol == '@') rowSb.Append($"{UIHelper.ColorRed}{symbol}{UIHelper.ColorReset}");
                else rowSb.Append(symbol);
            }
            mapContent.Add(rowSb.ToString());
        }
        allLines.AddRange(UIHelper.DrawBox("Map", mapContent, Width, 19));

        // dziennik
        var journalContent = new List<string>();
        var logs = gameState.GetLogHistory();
        foreach (var log in logs.TakeLast(8))
        {
            var wrappedLines = UIHelper.WrapText(log, Width - 4);
            foreach (var line in wrappedLines)
            {
                journalContent.Add($" {line}");
            }
        }
        allLines.AddRange(UIHelper.DrawBox("Journal", journalContent, Width, 10));

        return allLines;
    }
}