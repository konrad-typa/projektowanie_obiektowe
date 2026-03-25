using System.Text;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Services;
using Erpeg.Systems.CharacterSystems;

namespace Erpeg.Systems.GameStates;

public class ExplorationState : IGameState
{
    private readonly MapData _map;
    private readonly PlayerData _player;

    public ExplorationState(MapData map, PlayerData player)
    {
        _map = map;
        _player = player;
    }
    public void HandleInput(InputActionType action)
    {
        switch (action)
        {
            case InputActionType.MoveUp:
            case InputActionType.MoveDown:
            case InputActionType.MoveLeft:
            case InputActionType.MoveRight:
                PlayerMovement.HandleMovement(_map, _player, action);
                break;
            
            case InputActionType.PickUp:
                InventorySystem.TryPickUp(_map, _player);
                break;

            case InputActionType.OpenInventory:
                GameStateManager.ChangeState(new InventoryState(_map, _player));
                break;
            
            case InputActionType.Exit:
                Environment.Exit(0);
                break; // ZAMIEŃ NA GAMEOVER
        }
    }

    public void Update()
    {
        var info = _map.GetItemAt(_player.Position);
        if (info != null)
        {
            MessageLogSystem.Log($"({info.Name}) Pick Up [E]");
        }
        else
        {
            MessageLogSystem.Clear();
        }
    }

    public List<string> GetAvailableActions()
    {
        List<string> actionList = new();

        actionList.Add("Actions:");
        actionList.Add("Move: [W, A, S, D]");
        actionList.Add("Open Inventory: [I]");
        actionList.Add("Exit: [Esc]");
        
        return actionList;
    }
}