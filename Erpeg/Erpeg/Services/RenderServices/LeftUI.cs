using Erpeg.Data.Models.Characters;
using Erpeg.Core.Interfaces;

namespace Erpeg.Services.RenderServices;

public static class LeftUI
{
    public const int Width = 32;
    private static readonly string HoldNum = "??";

    public static List<string> Render(PlayerData player, IGameState gameState)
    {
        var allLines = new List<string>();

        var playerContent = new List<string>
        {
            UIHelper.PadOrCropAnsi($"  {UIHelper.ColorGreen}{player.Name}{UIHelper.ColorReset} " +
                                   $"lvl {HoldNum}", Width - 2),
            $"  Exp: 100%",
            $"  [{UIHelper.GetProgressBar(100, 100, 20, 
                UIHelper.ColorCyan, UIHelper.ColorDarkGray)}]",
            new('─', Width - 2),
            $"  HP: {player.Hp} / {player.MaxHp}",
            $"  [{UIHelper.GetProgressBar(player.Hp, player.MaxHp, 20, 
                UIHelper.ColorRed, UIHelper.ColorDarkGray)}]",
            $"  Mana: {player.Mana}/{player.MaxMana}",
            $"  [{UIHelper.GetProgressBar(player.Mana, player.MaxMana, 20, 
                UIHelper.ColorBlue, UIHelper.ColorDarkGray)}] ",
            new('─', Width - 2),
            $"  Strength:               {player.GetTotalAttribute(AttributesType.Strength)}",
            $"  Stamina:                {player.GetTotalAttribute(AttributesType.Stamina)}",
            $"  Luck:                   {player.GetTotalAttribute(AttributesType.Luck)}",
            $"  Intelligence:           {player.GetTotalAttribute(AttributesType.Intelligence)}",
            $"  Dexterity:              {player.GetTotalAttribute(AttributesType.Dexterity)}",
            $"  Aggression:             {player.GetTotalAttribute(AttributesType.Aggression)}"
        };
        allLines.AddRange(UIHelper.DrawBox("Player", playerContent, Width, 18));

        var actionContent = gameState.GetAvailableActions();
        allLines.AddRange(UIHelper.DrawBox("Available Actions", actionContent, Width, 10));

        return allLines;
    }
}