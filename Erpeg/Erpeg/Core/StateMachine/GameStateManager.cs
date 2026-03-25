using Erpeg.Services;

namespace Erpeg.Core.StateMachine;

public static class GameStateManager
{
    public static IGameState CurrentState { get; private set; }

    public static void Initialize(IGameState initialState)
    {
        CurrentState  = initialState;
        InputService.OnInput += HandleAction;
    }

    public static void ChangeState(IGameState newState)
    {
        CurrentState = newState;
    }
    
    private static void HandleAction(InputActionType action)
    {
        CurrentState.HandleInput(action);
    }
    
    public static void Update()
    {
        CurrentState.Update();
    }
}