using Erpeg.Data.Models.Characters;
using Erpeg.Core.Interfaces;
using Erpeg.Data.DTOs;

namespace Erpeg.Services.RenderServices;

public static class LeftUI
{
    public const int Width = 26;
    private static readonly string HoldNum = "??";

    public static List<string> Render(LocalGameStateDTO state)
    {
        var player = state.LocalPlayer;
        var allLines = new List<string>();

        var playerContent = new List<string>
        {
            UIHelper.PadOrCropAnsi($"  {UIHelper.ColorGreen}{player.Name}{UIHelper.ColorReset}", Width - 2),
            $"  Lvl: 01     exp: 05%",
            $"  {UIHelper.GetProgressBar(5, 100, 20, 
                UIHelper.BlueSlate, UIHelper.ColorDarkGray)}",
            UIHelper.ColorDarkGray + new string('─', Width - 2) + UIHelper.ColorReset,
            $"  HP:   {player.Hp}/{player.MaxHp}",
            $"  {UIHelper.GetProgressBar(player.Hp, player.MaxHp, 20, 
                UIHelper.ColorHpRed, UIHelper.ColorDarkGray)}",
            $"  Mana: {player.Mana}/{player.MaxMana}",
            $"  {UIHelper.GetProgressBar(player.Mana, player.MaxMana, 20, 
                UIHelper.ColorManaBlue, UIHelper.ColorDarkGray)}"
            
        };
        allLines.AddRange(UIHelper.DrawBox("Player", playerContent, Width, 8, 
            UIHelper.MutedTeal));

        var statsConntent = new List<string>
        {
            $"  Damage:           {player.Damage}",
            $"  Defense:          {player.Defense}",
            UIHelper.ColorDarkGray + new string('─', Width - 2) + UIHelper.ColorReset,
            $"  Strength:         {player.Strength}",
            $"  Stamina:          {player.Stamina}",
            $"  Luck:             {player.Luck}",
            $"  Intelligence:     {player.Intelligence}",
            $"  Dexterity:        {player.Dexterity}",
            $"  Aggression:       {player.Aggression}"
        };
        allLines.AddRange(UIHelper.DrawBox("Stats", statsConntent, Width, 12,
            UIHelper.MutedTeal));
        
        var actionContent = state.AvailableActions;
        allLines.AddRange(UIHelper.DrawBox("Available Actions", actionContent, Width, 5,
            UIHelper.MutedCobalt));

        return allLines;
    }
}