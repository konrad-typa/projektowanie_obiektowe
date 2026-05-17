using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;

namespace Erpeg.Systems.GameStateSystems;

public class ChangeStateCommand : ICommand
{
    private readonly IGameState _newState;
    private readonly PlayerSession _session;

    public ChangeStateCommand(IGameState newState, PlayerSession session)
    {
        _newState = newState;
        _session = session;
    }

    public void Execute()
    {
        _session.ChangeState(_newState);
    }
}