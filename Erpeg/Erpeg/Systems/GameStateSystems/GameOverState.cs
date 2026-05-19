using Erpeg.Core.Interfaces;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.View;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.GameStateSystems;

public class GameOverState(PlayerSession session) : IGameState
{
    private readonly PlayerSession _session = session;
    public void HandleInput(ConsoleKey key)
    {
        Environment.Exit(0);
    }

    public void Update() 
    { 
    }

    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            " ============================== ",
            "         GAME OVER            ",
            " ============================== ",
            "",
            "  Press any key ",
            "  to exit the game "
        };
    }

    public UIContext GetUIContext() => new UIContext();
    
    public List<(DateTime, string)> GetLogHistory() => _session.Logger.GetFullHistory();
}