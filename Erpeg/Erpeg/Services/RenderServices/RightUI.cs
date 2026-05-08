using Erpeg.Data.Models.Characters;

namespace Erpeg.Services.RenderServices;

public static class RightUI
{
    public const int Width = 38;

    public static List<string> Render(PlayerData player)
    {
        var allLines = new List<string>();

        var eqContent = new List<string>();
        foreach (var eq in player.Equipment)
        {
            string itemName = eq.Value?.Name ?? "None";
            eqContent.Add($" {eq.Key}: {UIHelper.ColorCyan}{itemName}{UIHelper.ColorReset}");
        }
        allLines.AddRange(UIHelper.DrawBox("Equipment", eqContent, Width, 10));

        var invContent = new List<string>
        {
            UIHelper.CenterAnsi($"Gold: {UIHelper.ColorYellow}{player.Gold}{UIHelper.ColorReset}    Coins: {UIHelper.ColorYellow}{player.Coins}{UIHelper.ColorReset}", Width - 2),
            new string('─', Width - 2),
            UIHelper.CenterAnsi("Weight : 10/50", Width - 2)
        };
        foreach (var item in player.Inventory)
        {
            invContent.Add($" {item.Name}"); 
        }
        allLines.AddRange(UIHelper.DrawBox("Inventory", invContent, Width, 12));

        var legendContent = new List<string>
        {
            " @ - Enemy        [1-9] - Players",
            " ! - TH Sword     O - Shield",
            " / - OH Sword     = - Wood",
            " . - Dust         } - Magic Weapon",
            " & - Armor        Ω - Artifact"
        };
        allLines.AddRange(UIHelper.DrawBox("Legend", legendContent, Width, 10));

        return allLines;
    }
}