using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;

namespace Erpeg.Systems.GameStateSystems;

public class InventoryState : IGameState
{
    private readonly MapData _map;
    private readonly PlayerData _player;
    private int _selectedIndex = 0;
    
    private readonly Dictionary<ConsoleKey, Action> _keyBindings;

    public InventoryState(MapData map, PlayerData player)
    {
        _map = map;
        _player = player;
        
        _keyBindings = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.A, () => _selectedIndex-- },
            { ConsoleKey.D, () => _selectedIndex++ },
            { ConsoleKey.E, UseSelectedItem },
            { ConsoleKey.G, DropSelectedItem },
            { ConsoleKey.I, () => GameStateManager.ChangeState(new ExplorationState(_map, _player)) },
            { ConsoleKey.Escape, () => GameStateManager.ChangeState(new ExplorationState(_map, _player)) }
        };
    }

    public void HandleInput(ConsoleKey key)
    {
        if (_keyBindings.TryGetValue(key, out Action action))
        {
            action.Invoke();
        }
    }

    private void UseSelectedItem()
    {
        if (_player.Inventory.Count > 0)
        {
            var item = _player.Inventory[_selectedIndex];
            item.Use(_player); 
            
            if (_selectedIndex >= _player.Inventory.Count) 
                _selectedIndex = Math.Max(0, _player.Inventory.Count - 1);
        }
    }
    
    private void DropSelectedItem()
    {
        if (_player.Inventory.Count > 0)
        {
            var item = _player.Inventory[_selectedIndex];
  
            if (_map.TryGetAvailableTile(_player.Position, out var dropPos))
            {
                _player.RemoveItemFromInventory(item);
                
                _map.Items[dropPos] = item;
            
                MessageLogSystem.Log($"Dropped {item.Name}.");
            
                if (_selectedIndex >= _player.Inventory.Count) 
                    _selectedIndex = Math.Max(0, _player.Inventory.Count - 1);
            }
            else
            {
                MessageLogSystem.Log("No space to drop the item here!");
            }
        }
    }

    public void Update()
    {
        if (_player.Inventory.Count > 0)
        {
            _selectedIndex = Math.Clamp(_selectedIndex, 0, _player.Inventory.Count - 1);
            var item =  _player.Inventory[_selectedIndex];
            MessageLogSystem.SetContext($"({item.Name}) Equip/Use [E] / Drop [G]");
        }
        else
        {
            MessageLogSystem.SetContext("Inventory empty!");
        }
    }
    
    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "Actions:",
            "Prev Item: [A]",
            "Next Item: [D]",
            "Close Inv: [I]/[Esc]"
        };
    }
}