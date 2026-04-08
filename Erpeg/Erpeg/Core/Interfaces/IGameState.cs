namespace Erpeg.Core.Interfaces;

public interface IGameState
{
    void HandleInput(ConsoleKey key);
    void Update();
    List<string> GetAvailableActions();
}