using Erpeg.Systems.LogSystem;
using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.GameStateSystems;

public class JournalState : IGameState
{
    private readonly MapData _map;
    private readonly PlayerData _player;
    private int _offset = 0;
    private static List<string> _fullHistory = GameLogger.Instance.GetFullHistory();
    
    private readonly Dictionary<ConsoleKey, Action> _keyBindings;

    public JournalState(MapData map, PlayerData player)
    {
        _map = map;
        _player = player;
        
        _keyBindings = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.A, () => _offset-- },
            { ConsoleKey.D, () => _offset++ },
            { ConsoleKey.J, () => GameStateManager.ChangeState(new ExplorationState(_map, _player)) },
            { ConsoleKey.Escape, () => GameStateManager.ChangeState(new ExplorationState(_map, _player)) }
        };
    }

    public void HandleInput(ConsoleKey key)
    {
        if (_keyBindings.TryGetValue(key, out Action action))
        {
            action.Invoke();
        }
        else
        {
            GameLogger.Instance.Log($"[{key}]: Wrong input");
        }
    }

    public void Update()
    {
    }
    
    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "Actions:",
            "Prev page: [A]",
            "Next page: [D]",
            "Close Journal: [I]/[Esc]"
        };
    }
}