using System.Text;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Services;

public static class UIService
{
    public static int Width = 60;
    public static List<string> GenerateUILinesRight(MapData map, PlayerData player)
    {
        List<string> hudLines = new();
        
        // atrybuty
        hudLines.Add("╔" + new string('=', Width - 2) + "╗");
        string statsLine0 =
            $"Health: {player.Hp}/{player.MaxHp}".PadRight(Width/2 - 5) +
            "  " +
            $"Mana: {player.Mana}/{player.MaxMana}".PadRight(Width/2 - 5);
        hudLines.Add("║" + Center(statsLine0, Width - 2) + "║ Player Stats");
        
        string statsLine1 =
            $"Damage: {player.Damage}".PadRight(Width/2 - 5) +
            "  " +
            $"Defense: {player.Defense}".PadRight(Width/2 - 5);
        hudLines.Add("║" + Center(statsLine1, Width - 2) + "║");
        
        hudLines.Add("║" + Center(" ", Width - 2) + "║");
        
        string statsLine2 =
            $"Strength: {player.Attributes[AttributesType.Strength]}".PadRight(Width/2 - 5) +
            "  " +
            $"Dexterity: {player.Attributes[AttributesType.Dexterity]}".PadRight(Width/2 - 5);
        hudLines.Add("║" + Center(statsLine2, Width - 2) + "║");
        
        string statsLine3 =
            $"Stamina: {player.Attributes[AttributesType.Stamina]}".PadRight(Width/2 - 5) +
            "  " +
            $"Intelligence: {player.Attributes[AttributesType.Intelligence]}".PadRight(Width/2 - 5);
        hudLines.Add("║" + Center(statsLine3, Width - 2) + "║");
        
        string statsLine4 =
            $"Luck: {player.Attributes[AttributesType.Luck]}".PadRight(Width/2 - 5) +
            "  " +
            $"Aggression: {player.Attributes[AttributesType.Aggression]}".PadRight(Width/2 - 5);
        hudLines.Add("║" + Center(statsLine4, Width - 2) + "║");
        
        hudLines.Add("╠" + new string('=', Width - 2) + "╣");
        
        // ekwipunek
        var eqList = player.Equipment.ToList();
        
        for (int i = 0; i < eqList.Count; i += 2)
        {
            var first = eqList[i];
            string firstText = $"{first.Key}: {(first.Value?.Name ?? "")}";
            string secondText = "";
            if (i + 1 < eqList.Count)
            {
                var second = eqList[i + 1];
                secondText = $"{second.Key}: {(second.Value?.Name ?? "")}";
            }
            string combinedLine = $"{firstText.PadRight(Width/2 - 5)}  {secondText.PadRight(Width/2 - 5)}";
            
            if (i == 0)
                hudLines.Add("║" + Center(combinedLine, Width - 2) + "║ EQ");
            else
                hudLines.Add("║" + Center(combinedLine, Width - 2) + "║");
        }
        hudLines.Add("╠" + new string('=', Width - 2) + "╣");
        
        // inventory - waluty
        hudLines.Add("║" + Center( "" +
                                   $"Coins: {player.Coins}     Gold: {player.Gold}", 
            Width - 2) + "║ Inventory");
        
        // inventory - itemy
        StringBuilder rowSb = new();
        foreach (var item in player.Inventory)
        {
            string itemName = item.Name;
            if (rowSb.Length + itemName.Length > Width - 3)
            {
                string fullRow = rowSb.ToString().TrimEnd(',', ' ');
                hudLines.Add("║" + Center(fullRow, Width - 2) + "║");
                rowSb.Clear();
            }
            rowSb.Append(itemName);
            rowSb.Append(", ");
        }
        if (rowSb.Length > 0)
        { 
            string lastRow = rowSb.ToString().TrimEnd(' ', ',');
            hudLines.Add("║" + Center(lastRow, Width - 2) + "║");
        }
        
        hudLines.Add("╚" + new string('=', Width - 2) + "╝");
        return hudLines;
    }

    public static List<string> GenerateUILinesTop(MapData map, PlayerData player)
    {
        List<string> hudLines = new();
        
        // informacja o przedmiocie na polu 
        var info = MessageLogSystem.GetContext();
        hudLines.Add(Center(info, Width * 2 - 10));
        
        return hudLines;
    }
    
    public static List<string> GenerateUILinesLeft(MapData map, PlayerData player, IGameState gameState)
    {
        List<string> hudLines = new();
    
        // FPS
        string fpsText = $"FPS: {GameDiagnostics.FPS}";
        hudLines.Add(fpsText.PadRight(Width / 2));
        hudLines.Add(" ".PadRight(Width / 2));
        
        // spis ruchów
        var actions = gameState.GetAvailableActions();
        foreach (var action in actions)
        {
            hudLines.Add(action.PadRight(Width / 2));
        }
        hudLines.Add(" ".PadRight(Width / 2));
        
        // historia ostatnich akcji
        hudLines.Add("History:".PadRight(Width / 2));
        var logs = MessageLogSystem.GetLogs();
        foreach (var log in logs)
        {
            var wrappedLines = WrapText(log, Width / 2 - 2);
            foreach (var line in wrappedLines)
            {
                hudLines.Add(line.PadRight(Width / 2));
            }
        }
        
        return hudLines;
    }
    
    public static List<string> GenerateUILinesBottom(MapData map, PlayerData player)
    {
        List<string> hudLines = new();
        
        return hudLines;
    }
    
    // helpery

    private static string Center(string? text, int width)
    {
        text ??= "";
        if (text.Length >= width) return text.Substring(0, width);

        int leftPadding = (width - text.Length) / 2;
        int rightPadding = width - text.Length - leftPadding;

        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }
    
    private static List<string> WrapText(string text, int maxWidth)
    {
        if (string.IsNullOrEmpty(text)) return new List<string>();

        var words = text.Split(' ');
        var lines = new List<string>();
        var currentLine = "";

        foreach (var word in words)
        {
            if (currentLine.Length + word.Length + 1 > maxWidth)
            {
                if (!string.IsNullOrEmpty(currentLine)) 
                    lines.Add(currentLine);
            
                currentLine = word;
            }
            else
            {
                currentLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
            }
        }
        
        if (!string.IsNullOrEmpty(currentLine)) 
            lines.Add(currentLine);

        return lines;
    }
}