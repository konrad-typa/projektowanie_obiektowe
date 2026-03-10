using System.ComponentModel.DataAnnotations;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems.CharacterSystems;

namespace Erpeg.Systems.GameStates;

public class InventoryState : IGameState
{
    private readonly MapData _map;
    private readonly PlayerData _player;
    private int _selectedIndex = 0;

    public InventoryState(MapData map, PlayerData player)
    {
        _map = map;
        _player = player;
    }
    public void HandleInput(InputActionType action)
    {
        switch (action)
        {
            case InputActionType.MoveLeft: 
                _selectedIndex = Math.Max(0, _selectedIndex - 1);
                break;
            case InputActionType.MoveRight:
                _selectedIndex = Math.Min(_player.Inventory.Count - 1, _selectedIndex + 1);
                break;
            
            case InputActionType.PickUp:
                if (_player.Inventory.Count > 0)
                    InventorySystem.TryEquip(_player, _player.Inventory[_selectedIndex]);
                break;

            case InputActionType.Drop:
                if (_player.Inventory.Count > 0)
                    InventorySystem.TryDrop(_map, _player, _player.Inventory[_selectedIndex]);
                break;
            
            case InputActionType.OpenInventory:
            case InputActionType.Exit:
                GameStateManager.ChangeState(new ExplorationState(_map, _player));
                break;
        }
    }

    public void Update()
    {
        if (_player.Inventory.Count > 0)
        {
            var item =  _player.Inventory[_selectedIndex];
            MessageLogSystem.Log($"({item.Name}) Equip [E] / Drop [G]");
        }
        else
        {
            MessageLogSystem.Log("Inventory empty!");
        }
    }
}