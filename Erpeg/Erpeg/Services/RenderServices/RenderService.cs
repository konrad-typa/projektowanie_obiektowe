using System.Text;
using Erpeg.Core.Interfaces;
using Erpeg.Data.DTOs;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;

namespace Erpeg.Services.RenderServices;

public class RenderService : IService
{
    public void Initialize() { }

    public static string RenderFrame(GameStateDto state)
    {
        var sb = new StringBuilder();
        
        var leftColumn = LeftUI.Render(state);
        var centerColumn = CenterUI.Render(state);
        var rightColumn = RightUI.Render(state);
        
        int maxHeight = Math.Max(leftColumn.Count, Math.Max(centerColumn.Count, rightColumn.Count));

        for (int i = 0; i < maxHeight; i++)
        {
            string left = i < leftColumn.Count ? leftColumn[i] : new string(' ', LeftUI.Width);
            string center = i < centerColumn.Count ? centerColumn[i] : new string(' ', CenterUI.Width);
            string right = i < rightColumn.Count ? rightColumn[i] : new string(' ', RightUI.Width);
            
            sb.AppendLine($"{left} {center} {right}");
        }
        
        int totalWidth = LeftUI.Width + CenterUI.Width + RightUI.Width + 6; 
        
        // sb.AppendLine(new string('─', totalWidth));
        string footer = $"FPS: {GameDiagnostics.FPS}                   " +
                        $"Time: {GameDiagnostics.PlayTime.Elapsed:mm\\:ss}" +
                        $"                   Score: ???                   Active Players: 1";
        string formattedFooter = $" {UIHelper.CenterAnsi(footer, totalWidth - 5)} ";
        sb.AppendLine(formattedFooter);
        // sb.AppendLine(new string('─', totalWidth));

        return sb.ToString();
    }
}