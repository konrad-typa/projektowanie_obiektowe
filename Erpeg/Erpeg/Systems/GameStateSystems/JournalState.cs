using Erpeg.Systems.LogSystem;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Data.Models.View;

namespace Erpeg.Systems.GameStateSystems;

public class JournalState : IGameState
{
    private int _offset = int.MaxValue;
    private const int WindowSize = 5;
    private readonly PlayerSession _session;
    
    private readonly Dictionary<ConsoleKey, Action> _keyBindings;

    public JournalState(MapData map, PlayerSession session)
    {
        _session = session;
        
        _keyBindings = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.W, () => _offset-- },
            { ConsoleKey.S, () => _offset++ },
            { ConsoleKey.J, () => _session.ChangeState(new ExplorationState(map, _session)) },
            { ConsoleKey.Escape, () => _session.ChangeState(new ExplorationState(map, _session)) },
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
            _session.Logger.Log($"[{key}] Wrong input");
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
        var fullHistory = _session.Logger.GetFullHistory();
        _offset = Math.Clamp(_offset, 0, Math.Max(0,  fullHistory.Count - WindowSize));

        return fullHistory.Skip(_offset).Take(WindowSize).ToList();
    } 
}