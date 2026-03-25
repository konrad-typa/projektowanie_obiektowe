using Erpeg.Services;

public interface IGameState
{
    void HandleInput(InputActionType action);
    void Update();
    List<string> GetAvailableActions();
}