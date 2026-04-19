using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.CharacterSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.GameStateSystems;

public class ExplorationState : IGameState
{
    private readonly MapData _map;
    private readonly PlayerData _player;
    private readonly Dictionary<ConsoleKey, ICommand> _commands;
    public ExplorationState(MapData map, PlayerData player)
    {
        _map = map;
        _player = player;
        
        _commands = new Dictionary<ConsoleKey, ICommand>
        {
            { ConsoleKey.W, new MoveCommand(_map, _player, 0, -1) },
            { ConsoleKey.S, new MoveCommand(_map, _player, 0, 1) },
            { ConsoleKey.A, new MoveCommand(_map, _player, -1, 0) },
            { ConsoleKey.D, new MoveCommand(_map, _player, 1, 0) },
            { ConsoleKey.E, new PickUpCommand(_map, _player) },
            { ConsoleKey.I, new ChangeStateCommand(new InventoryState(_map, _player)) },
            { ConsoleKey.J, new ChangeStateCommand(new JournalState(_map, _player)) },
            { ConsoleKey.Escape, new ChangeStateCommand(new GameOverState()) },
        };
    }

    public void HandleInput(ConsoleKey key)
    {
        if (_commands.TryGetValue(key, out ICommand command))
        {
            command.Execute();
        }
        else
        {
            GameLogger.Instance.Log($"[{key}]: Wrong input");
        }
    }

    public void Update()
    {
        var info = _map.GetItemAt(_player.Position);
        if (info != null)
        {
            GameLogger.Instance.SetContext($"({info.Name}) Pick Up [E]");
        }
        else
        {
            GameLogger.Instance.SetContext("");
        }
    }

    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "Actions:",
            "Move: [W, A, S, D]",
            "Pick Up: [E]",
            "Inventory: [I]",
            "Exit: [Esc]"
        };
    }

    public List<string> GetLogHistory() => GameLogger.Instance.GetRecentLogs();
}