using Erpeg.Core.Interfaces;

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
            "==============================",
            "         GAME OVER            ",
            "==============================",
            "",
            "Press any key to exit the game"
        };
    }
}