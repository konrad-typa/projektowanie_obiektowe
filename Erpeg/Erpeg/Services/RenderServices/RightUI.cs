using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;

namespace Erpeg.Services.RenderServices;

public static class RightUI
{
    public const int Width = 38;

    public static List<string> Render(PlayerData player, IGameState gameState)
    {
        var allLines = new List<string>();

        var eqContent = new List<string>();
        foreach (var eq in player.Equipment)
        {
            string itemName = eq.Value?.Name ?? "None";
            string leftText = $"  {eq.Key}:";
            string rightText = $"{UIHelper.ColorCyan}{itemName}{UIHelper.ColorReset}";

            eqContent.Add(UIHelper.JustifyAnsi(leftText, rightText, Width - 2, rightPadding: 2));
        }
        allLines.AddRange(UIHelper.DrawBox("Equipment", eqContent, Width, 6));

        var invContent = new List<string>
        {
            UIHelper.CenterAnsi($"Gold: {UIHelper.ColorYellow}{player.Gold}{UIHelper.ColorReset}    " +
                                $"Coins: {UIHelper.ColorYellow}{player.Coins}{UIHelper.ColorReset}", Width - 2),
            new string('─', Width - 2),
            UIHelper.CenterAnsi($"{player.CurrentWeight}/{player.MaxWeight}", Width - 2)
        };
        
        var interactiveInv = gameState.GetInteractiveInventory();
        if (interactiveInv != null)
        {
            foreach (var item in interactiveInv)
            {
                invContent.Add(item);
            }
        }
        else
        {
            int itemsToShow = Math.Min(10, player.Inventory.Count);
            for (int i = 0; i < itemsToShow; i++)
            {
                var item = player.Inventory[i];
                string name = $"  {item.Name}";
                string weight = $"{item.Weight}";
                
                invContent.Add(UIHelper.JustifyAnsi(name, weight, Width - 2, rightPadding: 2));
            }
            
            if (player.Inventory.Count > 10)
                invContent.Add($"  ... ({player.Inventory.Count - 10} more)");
        }
        
        allLines.AddRange(UIHelper.DrawBox("Inventory", invContent, Width, 14));

        var legendContent = new List<string>
        {
            " @ - Enemy        [1-9] - Players",
            " ! - TH Sword     O - Shield",
            " / - OH Sword     =* - Ores",
            " . - Dust         } - Magic Weapon",
            " & - Armor        Ω - Artifact"
        };
        allLines.AddRange(UIHelper.DrawBox("Legend", legendContent, Width, 5));

        return allLines;
    }
}