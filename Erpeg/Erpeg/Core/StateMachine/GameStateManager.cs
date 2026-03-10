using Erpeg.Services;

namespace Erpeg.Core.StateMachine;

public static class GameStateManager
{
    private static IGameState _currentState;

    public static void Initialize(IGameState initialState)
    {
        _currentState  = initialState;
        InputService.OnInput += HandleAction;
    }

    public static void ChangeState(IGameState newState)
    {
        _currentState = newState;
    }
    
    private static void HandleAction(InputActionType action)
    {
        _currentState.HandleInput(action);
    }
    
    public static void Update()
    {
        _currentState.Update();
    }
}