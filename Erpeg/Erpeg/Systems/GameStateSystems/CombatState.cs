using System;
using System.Collections.Generic;
using Erpeg.Core.Interfaces;
using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;
using Erpeg.Systems.CombatSystems;
using Erpeg.Systems.GameStateSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.GameStates;

public class CombatState : IGameState
{
    private readonly MapData _map;
    private readonly PlayerData _player;
    private readonly EnemyData _enemy;
    private readonly Dictionary<ConsoleKey, Action> _combatActions;
    
    private readonly Item _fists = new FistsItem();

    public CombatState(MapData map, PlayerData player, EnemyData enemy)
    {
        _map = map;
        _player = player;
        _enemy = enemy;
        
        GameLogger.Instance.SetContext($"Combat! {_enemy.Name} blocks the way.");

        _combatActions = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.D1, () => ExecuteTurn(new NormalAttack()) },
            { ConsoleKey.D2, () => ExecuteTurn(new StealthAttack()) },
            { ConsoleKey.D3, () => ExecuteTurn(new MagicAttack()) }
        };
    }

    public void HandleInput(ConsoleKey key)
    {
        if (_combatActions.TryGetValue(key, out Action action))
        {
            action.Invoke();
        }
        else
        {
            GameLogger.Instance.Log($"[{key}]: Wrong input");
        }
    }

    private void ExecuteTurn(IAttackVisitor attackType)
    {
        var weapon = _player.Equipment.TryGetValue(EquipmentSlotType.MainHand, out var w) && w != null ? w : _fists;
        
        int playerDamage = weapon.AcceptDamage(attackType, _player, weapon.Damage);
        int playerDefense = weapon.AcceptDefense(attackType, _player, weapon.Defense);
        
        int damageToEnemy = Math.Max(0, playerDamage - _enemy.Defense);
        _enemy.Hp -= damageToEnemy;
        GameLogger.Instance.Log($"You hit {_enemy.Name} for {damageToEnemy} dmg!");

        if (_enemy.Hp <= 0)
        {
            GameLogger.Instance.Log($"{_enemy.Name} has been defeated!");
            _map.Characters.Remove(_enemy);
            GameStateManager.ChangeState(new ExplorationState(_map, _player));
            return;
        }
        
        int damageToPlayer = Math.Max(0, _enemy.Attack - playerDefense);
        _player.Hp -= damageToPlayer; 
        GameLogger.Instance.Log($"{_enemy.Name} hits you for {damageToPlayer} dmg!");
        
        if (_player.Hp <= 0)
        {
            GameLogger.Instance.Log("YOU DIED!");
            GameStateManager.ChangeState(new GameOverState()); 
        }
    }

    public void Update() { }

    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "== COMBAT ==",
            $"Target: {_enemy.Name}",
            $"HP: {_enemy.Hp}/{_enemy.MaxHp}",
            "",
            "Actions:",
            "[1] Normal Attack",
            "[2] Stealth Attack",
            "[3] Magic Attack"
        };
    }
    
    public List<string> GetLogHistory() => GameLogger.Instance.GetRecentLogs();
}