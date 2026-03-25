using System.Text;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Services;

public static class UIService
{
    public static int Width = 50;
    public static List<string> GenerateUILinesRight(MapData map, PlayerData player)
    {
        List<string> hudLines = new();
        
        // atrybuty
        hudLines.Add("╔" + new string('=', Width - 2) + "╗");
        hudLines.Add("║" + Center( "Player Stats", Width - 2) + "║");
        
        string statsLine1 =
            $"Strength: {player.Attributes[AttributesType.Strength]}".PadRight(Width/2 - 5) +
            "  " +
            $"Dexterity: {player.Attributes[AttributesType.Dexterity]}".PadRight(Width/2 - 5);
        hudLines.Add("║" + Center(statsLine1, Width - 2) + "║");
        
        string statsLine2 =
            $"Stamina: {player.Attributes[AttributesType.Stamina]}".PadRight(Width/2 - 5) +
            "  " +
            $"Intelligence: {player.Attributes[AttributesType.Intelligence]}".PadRight(Width/2 - 5);
        hudLines.Add("║" + Center(statsLine2, Width - 2) + "║");
        
        hudLines.Add("╠" + new string('=', Width - 2) + "╣");
        
        // ekwipunek
        hudLines.Add("║" + Center( "Equipment", Width - 2) + "║");
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
            
            hudLines.Add("║" + Center(combinedLine, Width - 2) + "║");
        }
        hudLines.Add("╠" + new string('=', Width - 2) + "╣");
        
        // inventory - waluty
        hudLines.Add("║" + Center( "" +
                                   $"Coins: {player.Money[ItemType.Coin]}     Gold: {player.Money[ItemType.Gold]}", 
            Width - 2) + "║");
        
        //inventory - itemy
        StringBuilder rowSb = new();
        foreach (var item in player.Inventory)
        {
            if (item.Type == ItemType.Gold || item.Type == ItemType.Coin)
                continue;
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
        var info = MessageLogSystem.GetCurrent();
        hudLines.Add(Center(info, Width * 2));
        
        return hudLines;
    }
    
    public static List<string> GenerateUILinesLeft(MapData map, PlayerData player, IGameState gameState)
    {
        List<string> hudLines = new();
    
        // FPS
        string fpsText = $"FPS: {GameDiagnostics.FPS}";
        hudLines.Add(fpsText.PadRight(Width/2));
        hudLines.Add(" ".PadRight(Width/2));
        
        // spis ruchów
        var actions = gameState.GetAvailableActions();
        foreach (var action in actions)
        {
            hudLines.Add(action.PadRight(Width/2));
        }
        
        return hudLines;
    }
    
    public static List<string> GenerateUILinesBottom(MapData map, PlayerData player)
    {
        List<string> hudLines = new();
        
        return hudLines;
    }

    private static string Center(string? text, int width)
    {
        text ??= "";
        if (text.Length >= width) return text.Substring(0, width);

        int leftPadding = (width - text.Length) / 2;
        int rightPadding = width - text.Length - leftPadding;

        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }
}