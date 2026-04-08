using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;

namespace Erpeg.Systems.GameStateSystems;

public class ChangeStateCommand : ICommand
{
    private readonly IGameState _newState;

    public ChangeStateCommand(IGameState newState)
    {
        _newState = newState;
    }

    public void Execute()
    {
        GameStateManager.ChangeState(_newState);
    }
}