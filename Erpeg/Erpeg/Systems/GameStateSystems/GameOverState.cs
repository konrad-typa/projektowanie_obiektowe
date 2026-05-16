using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.View;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.GameStateSystems;

public class GameOverState : IGameState
{
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
    
    public List<(DateTime, string)> GetLogHistory() => GameLogger.Instance.GetFullHistory();
}