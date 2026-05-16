using System.Security.Cryptography;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Data.Models.View;
using Erpeg.Systems.CharacterSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.GameStateSystems;

public class ExplorationState : IGameState
{
    private readonly MapData _map;
    private readonly PlayerData _player;
    private readonly Dictionary<ConsoleKey, ICommand> _commands;
    private readonly UIContext _uiContext = new UIContext();
    
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
            { ConsoleKey.I, new ChangeStateCommand(new InventoryState(_map, _player, this))},
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
            GameLogger.Instance.Log($"[{key}] Wrong input");
        }
    }

    public void Update()
    {
        var enemies = _map.Characters.Values.OfType<EnemyData>().ToList();
        foreach (var e in enemies)
            e.MoveRandomly(_map);
    }
    
    public UIContext GetUIContext()
    {
        var item = _map.GetItemAt(_player.Position);
        if (item != null)
        {
            _uiContext.message = $"({item.Name}) Pick Up [E]";
        }
        else 
            _uiContext.message = "";
        return _uiContext;
    }

    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "  [W, A, S, D]: Move",
            "  [E] Pick Up",
            "  [I] Inventory",
            "  [J] Journal",
            "  [Esc] Exit"
        };
    }

    public List<(DateTime, string)> GetLogHistory() => GameLogger.Instance.GetFullHistory();
}