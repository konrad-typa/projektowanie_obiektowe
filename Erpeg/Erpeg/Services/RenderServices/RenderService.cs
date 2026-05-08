using System.Text;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Services.RenderServices;

public class RenderService : IService
{
    public void Initialize() { }

    public static string RenderFrame(MapData map, PlayerData player, IGameState gameState)
    {
        var sb = new StringBuilder();
        
        var leftColumn = LeftUI.Render(player, gameState);
        var centerColumn = CenterUI.Render(map, gameState);
        var rightColumn = RightUI.Render(player);
        
        int maxHeight = Math.Max(leftColumn.Count, Math.Max(centerColumn.Count, rightColumn.Count));

        for (int i = 0; i < maxHeight; i++)
        {
            string left = i < leftColumn.Count ? leftColumn[i] : new string(' ', LeftUI.Width);
            string center = i < centerColumn.Count ? centerColumn[i] : new string(' ', CenterUI.Width);
            string right = i < rightColumn.Count ? rightColumn[i] : new string(' ', RightUI.Width);
            
            sb.AppendLine($"{left}   {center}   {right}");
        }
        
        int totalWidth = LeftUI.Width + CenterUI.Width + RightUI.Width + 6; 
        
        sb.AppendLine(new string('─', totalWidth));
        
        string footer = $"FPS: {GameDiagnostics.FPS}          Czas gry: {UIHelper.PlaceHolder}" +
                        $"          Score: {UIHelper.PlaceHolder}          Active Players: {UIHelper.PlaceHolder}";
        string formattedFooter = $" {UIHelper.CenterAnsi(footer, totalWidth - 2)} ";
        
        sb.AppendLine(formattedFooter);
        sb.AppendLine(new string('─', totalWidth));

        return sb.ToString();
    }
}