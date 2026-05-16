using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Data.Models.View;
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
    private readonly UIContext _uiContext = new UIContext();
    private readonly InventoryInfo _inventoryInfo = new InventoryInfo()
    {
        isOpen = true,
        WindowSize = WindowSize
    };
    
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
            GameLogger.Instance.Log($"[{key}] Wrong input");
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
            if (_selectedIndex < _offset) 
                _offset = _selectedIndex; 
            else if (_selectedIndex >= _offset + WindowSize) 
                _offset = _selectedIndex - WindowSize + 1; 
        
            _offset = Math.Clamp(_offset, 0, Math.Max(0, _player.Inventory.Count - WindowSize));
        }
    }
    
    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "  Prev Item: [W]",
            "  Next Item: [S]",
            "  Close Inv: [I]/[Esc]"
        };
    }

    public UIContext GetUIContext()
    {
        if (_player.Inventory.Count > 0)
        {
            var item = _player.Inventory[_selectedIndex];
            _uiContext.message = $"({item.Name}) Equip [E] / Drop [G]";
        }
        else 
            _uiContext.message = "Inventory Empty!";
        return _uiContext;
    }
    
    public InventoryInfo GetInventoryInfo()
    {
        _inventoryInfo.selectedIdx = _selectedIndex;
        _inventoryInfo.Offset = _offset;
        return _inventoryInfo;
    }
    
    public List<(DateTime, string)> GetLogHistory() => GameLogger.Instance.GetFullHistory();
}