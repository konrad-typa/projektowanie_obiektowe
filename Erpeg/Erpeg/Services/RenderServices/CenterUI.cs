using System.Data;
using System.Text;
using Erpeg.Core.Interfaces;
using Erpeg.Data.DTOs;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Services.RenderServices;

public static class CenterUI
{
    public const int Width = 48;

    public static List<string> Render(GameStateDto state)
    {
        var allLines = new List<string>();
        var map = state.Map;

        // info
        var uiContext = state.UIContext;
        string contextText = uiContext.Message;
        if (uiContext.ShowBar)
        {
            var bar = UIHelper.GetProgressBar(uiContext.BarCurrent, uiContext.BarMax,
                15, UIHelper.ColorHpRed, UIHelper.ColorDarkGray);
            contextText += $" {bar} {uiContext.BarCurrent} / {uiContext.BarMax}";
        }
        
        var infoContent = new List<string> { UIHelper.CenterAnsi(contextText, Width - 2) };
        allLines.AddRange(UIHelper.DrawBox("Info", infoContent, Width, 1, UIHelper.MutedCobalt));
        
        // mapa
        var itemsDict = map.Items.ToDictionary(i => (i.X, i.Y));
        var charsDict = map.Characters.ToDictionary(c => (c.X, c.Y));
        var mapContent = new List<string>();
        
        for (int y = 0; y < map.SizeY; y++)
        {
            var rowSb = new StringBuilder();
            int padding = (Width - 2 - map.SizeX) / 2;
            rowSb.Append(new string(' ', Math.Max(0, padding)));

            for (int x = 0; x < map.SizeX; x++)
            {
                char symbol = map.Tiles[x][y] switch
                {
                    TileType.Wall => '█',
                    TileType.Empty => ' ',
                    _ => '?'
                };
                string color = UIHelper.FrameStone;

                if (itemsDict.TryGetValue((x, y), out var item))
                {
                    symbol = item.Symbol;
                    color = item.Color;
                }

                if (charsDict.TryGetValue((x, y), out var character))
                {
                    symbol = character.Symbol;
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
        foreach (var msg in state.RecentLogs.TakeLast(5))
        {
            var wrappedLines = UIHelper.WrapText(msg, Width - 4);
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