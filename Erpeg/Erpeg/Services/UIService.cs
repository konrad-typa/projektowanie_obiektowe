using System.Text;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Services;

public static class UIService
{
    public static List<string> GenerateUILines(MapData map, PlayerData player)
    {
        int width = 50;
        List<string> hudLines = new();

        // informacja o przedmiocie na polu
        var info= MessageLogSystem.GetCurrent();
        hudLines.Add(Center(info, width));
        
        // atrybuty
        hudLines.Add("╔" + new string('=', width - 2) + "╗");
        hudLines.Add("║" + Center( "Player Stats", width - 2) + "║");
        
        string statsLine1 =
            $"Strength: {player.Attributes[AttributesType.Strength]}".PadRight(width/2 - 5) +
            "  " +
            $"Dexterity: {player.Attributes[AttributesType.Dexterity]}".PadRight(width/2 - 5);
        hudLines.Add("║" + Center(statsLine1, width - 2) + "║");
        
        string statsLine2 =
            $"Stamina: {player.Attributes[AttributesType.Stamina]}".PadRight(width/2 - 5) +
            "  " +
            $"Intelligence: {player.Attributes[AttributesType.Intelligence]}".PadRight(width/2 - 5);
        hudLines.Add("║" + Center(statsLine2, width - 2) + "║");
        
        hudLines.Add("╠" + new string('=', width - 2) + "╣");
        
        // ekwipunek
        hudLines.Add("║" + Center( "Equipment", width - 2) + "║");
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
            string combinedLine = $"{firstText.PadRight(width/2 - 5)}  {secondText.PadRight(width/2 - 5)}";
            
            hudLines.Add("║" + Center(combinedLine, width - 2) + "║");
        }
        hudLines.Add("╠" + new string('=', width - 2) + "╣");
        
        // inventory - waluty
        hudLines.Add("║" + Center( "" +
                                   $"Coins: {player.Money[ItemType.Coin]}     Gold: {player.Money[ItemType.Gold]}", 
            width - 2) + "║");
        
        //inventory - itemy
        StringBuilder rowSb = new();
        foreach (var item in player.Inventory)
        {
            if (item.Type == ItemType.Gold || item.Type == ItemType.Coin)
                continue;
            string itemName = item.Name;
            if (rowSb.Length + itemName.Length > width - 3)
            {
                string fullRow = rowSb.ToString().TrimEnd(',', ' ');
                hudLines.Add("║" + Center(fullRow, width - 2) + "║");
                rowSb.Clear();
            }
            rowSb.Append(itemName);
            rowSb.Append(", ");
        }
        if (rowSb.Length > 0)
        { 
            string lastRow = rowSb.ToString().TrimEnd(' ', ',');
            hudLines.Add("║" + Center(lastRow, width - 2) + "║");
        }
        
        hudLines.Add("╚" + new string('=', width - 2) + "╝");
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