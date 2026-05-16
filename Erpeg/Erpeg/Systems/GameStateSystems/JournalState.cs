using Erpeg.Systems.LogSystem;
using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Data.Models.View;

namespace Erpeg.Systems.GameStateSystems;

public class JournalState : IGameState
{
    private int _offset = 0;
    private const int WindowSize = 10;
    
    private readonly Dictionary<ConsoleKey, Action> _keyBindings;

    public JournalState(MapData map, PlayerData player)
    {
        var map1 = map;
        var player1 = player;
        
        _keyBindings = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.W, () => _offset-- },
            { ConsoleKey.S, () => _offset++ },
            { ConsoleKey.J, () => GameStateManager.ChangeState(new ExplorationState(map1, player1)) },
            { ConsoleKey.Escape, () => GameStateManager.ChangeState(new ExplorationState(map1, player1)) }
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
            GameLogger.Instance.Log($"[{key}] Wrong input");
        }
    }

    public void Update()
    {
    }
    
    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "  [W] Scroll Up",
            "  [S] Scroll Down",
            "  [J]/[Esc] Close Journal"
        };
    }
    
    public UIContext GetUIContext() => new UIContext() { message = "Opened Journal" };

    public List<(DateTime, string)> GetLogHistory()
    {
        var fullHistory = GameLogger.Instance.GetFullHistory();
        _offset = Math.Clamp(_offset, 0, Math.Max(0,  fullHistory.Count - WindowSize));
        int bufferSize =(fullHistory.Count - 1 - _offset) < WindowSize ? fullHistory.Count - 1 : WindowSize;

        return fullHistory.Skip(_offset).Take(bufferSize).ToList();
    } 
}