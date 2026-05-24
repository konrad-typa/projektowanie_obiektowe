using Erpeg.Core.Interfaces;
using Erpeg.Data.DTOs;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Services.RenderServices;

public static class RightUI
{
    public const int Width = 38;

    public static List<string> Render(GameStateDTO state)
    {
        var player = state.LocalPlayer;
        var allLines = new List<string>();

        var eqContent = new List<string>();
        foreach (var eq in player.Equipment)
        {
            string itemName = eq.Value?.Name ?? "None";
            string leftText = $"  {eq.Key}:";
            string rightText = $"{UIHelper.ColorCyan}{itemName}{UIHelper.ColorReset}";

            eqContent.Add(UIHelper.JustifyAnsi(leftText, rightText, Width - 2, rightPadding: 2));
        }
        allLines.AddRange(UIHelper.DrawBox("Equipment", eqContent, Width, 6, 
            UIHelper.DarkCyan));

        var invContent = new List<string>
        {
            UIHelper.CenterAnsi($"{UIHelper.ColorGold}$ Gold: {player.Gold}{UIHelper.ColorReset}    " +
                                $"{UIHelper.ColorSilver}◎ Coins: {player.Coins}{UIHelper.ColorReset}", Width - 2),
            UIHelper.ColorDarkGray + new string('─', Width - 2) + UIHelper.ColorReset,
            UIHelper.CenterAnsi($"{player.CurrentWeight}/{player.MaxWeight}", Width - 2)
        };

        var inventoryInfo = state.InventoryInfo;
        var inv = player.Inventory;
        int offset = state.InventoryInfo.IsOpen ? inventoryInfo.Offset : 0;
        int windowSize = state.InventoryInfo.IsOpen ? inventoryInfo.WindowSize : 11;
        int selectedIdx = state.InventoryInfo.IsOpen ? state.InventoryInfo.SelectedIdx : -1;
        
        var itemsToShow = inv.Skip(offset).Take(windowSize).ToList();

        for (int i = 0; i < itemsToShow.Count; i++)
        {
            var item = itemsToShow[i];
            int realIndex = offset + i;
            
            bool isSelected = (realIndex == selectedIdx);
            
            string leftText = isSelected 
                ? $"> {item.Color}{item.Symbol}{UIHelper.ColorReset} {UIHelper.ColorGreen}{item.Name}{UIHelper.ColorReset}" 
                : $"  {item.Color}{item.Symbol}{UIHelper.ColorReset} {item.Name}";
                    
            string rightText = item.Weight.ToString();
            
            invContent.Add(UIHelper.JustifyAnsi(leftText, rightText, Width - 2, 2));
        }
        
        allLines.AddRange(UIHelper.DrawBox("Inventory", invContent, Width, 14, 
            UIHelper.DarkCyan));

        var legendContent = new List<string>
        {
            "  @ - Enemy        [1-9] - Players",
            "  ! - TH Sword     O - Shield",
            "  / - OH Sword     =* - Ores",
            "  . - Dust         } - Magic Weapon",
            "  & - Armor        Ω - Artifact"
        };
        allLines.AddRange(UIHelper.DrawBox("Legend", legendContent, Width, 5, 
            UIHelper.MutedCobalt));

        return allLines;
    }
}