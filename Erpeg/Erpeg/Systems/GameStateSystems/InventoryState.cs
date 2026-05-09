using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services.RenderServices;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.GameStateSystems;

public class InventoryState : IGameState
{
    private readonly MapData _map;
    private readonly PlayerData _player;
    private int _selectedIndex = 0;
    private const int WindowSize = 11;
    private int _offset = 0;
    
    private readonly Dictionary<ConsoleKey, Action> _keyBindings;

    public InventoryState(MapData map, PlayerData player, IGameState previousState)
    {
        _map = map;
        _player = player;
        
        _keyBindings = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.W, () => _selectedIndex-- },
            { ConsoleKey.S, () => _selectedIndex++ },
            { ConsoleKey.E, UseSelectedItem },
            { ConsoleKey.G, DropSelectedItem },
            { ConsoleKey.I, () => GameStateManager.ChangeState(previousState) },
            { ConsoleKey.Escape, () => GameStateManager.ChangeState(previousState) }
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
            
                GameLogger.Instance.Log($"Dropped {item.Name}.");
            
                if (_selectedIndex >= _player.Inventory.Count) 
                    _selectedIndex = Math.Max(0, _player.Inventory.Count - 1);
            }
            else
            {
                GameLogger.Instance.Log("No space to drop the item here!");
            }
        }
    }

    public void Update()
    {
        _selectedIndex = Math.Clamp(_selectedIndex, 0, Math.Max(0, _player.Inventory.Count - 1));

        if (_player.Inventory.Count > 0)
        {
            var selectedItem = _player.Inventory[_selectedIndex];
            GameLogger.Instance.SetContext($"({selectedItem.Name}) Equip [E] / Drop [G]");
        }
        else
        {
            GameLogger.Instance.SetContext("Inventory empty!");
        }
    }
    
    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "  Prev Item: [A]",
            "  Next Item: [D]",
            "  Close Inv: [I]/[Esc]"
        };
    }
    
    public List<string>? GetInteractiveInventory()
    {
        var inv = _player.Inventory;
        if (inv.Count == 0) 
            return new List<string>(); 
        
        _selectedIndex = Math.Clamp(_selectedIndex, 0, Math.Max(0, inv.Count - 1));
        
        if (_selectedIndex < _offset) 
            _offset = _selectedIndex; 
        else if (_selectedIndex >= _offset + WindowSize) 
            _offset = _selectedIndex - WindowSize + 1; 
        
        _offset = Math.Clamp(_offset, 0, Math.Max(0, inv.Count - WindowSize));
        
        return inv
            .Skip(_offset)
            .Take(WindowSize)
            .Select((item, index) => 
            {
                int realIndex = _offset + index; 
                
                string leftText = realIndex == _selectedIndex 
                    ? $"> {UIHelper.ColorGreen}{item.Name}{UIHelper.ColorReset}" 
                    : $"  {item.Name}";
                string rightText = item.Weight.ToString(); 
                
                return UIHelper.JustifyAnsi(leftText, rightText, RightUI.Width - 2, 2);
            })
            .ToList();
    }
    
    public List<string> GetLogHistory() => GameLogger.Instance.GetFullHistory();
}