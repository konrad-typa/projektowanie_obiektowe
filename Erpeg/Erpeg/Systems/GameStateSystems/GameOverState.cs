using Erpeg.Core.Interfaces;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.View;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.GameStateSystems;

public class GameOverState(PlayerSession session) : IGameState
{
    public void HandleInput(ConsoleKey key)
    {
    }

    public void Update()
    {
        
    }

    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            " ======================",
            "       GAME OVER       ",
            " ======================",
            "",
            "  You are Spectating "
        };
    }

    public UIContext GetUIContext() => new UIContext();
    
    public List<(DateTime, string)> GetLogHistory() => session.Logger.GetRecentLogs();
}