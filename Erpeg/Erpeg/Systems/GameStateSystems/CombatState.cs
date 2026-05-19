using System;
using System.Collections.Generic;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Models;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Data.Models.View;
using Erpeg.Services.RenderServices;
using Erpeg.Systems;
using Erpeg.Systems.CombatSystems;
using Erpeg.Systems.GameStateSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.GameStates;

public class CombatState : IGameState
{
    private static readonly Random Random = new();
    private readonly MapData _map;
    private readonly PlayerData _player;
    private readonly PlayerSession _session;
    private readonly EnemyData _enemy;
    private readonly Dictionary<ConsoleKey, Action> _combatActions;
    private readonly UIContext _uiContext = new UIContext { showBar = true };
    
    private readonly Item _fists = new FistsItem();

    public CombatState(MapData map, PlayerSession session, EnemyData enemy)
    {
        _map = map;
        _session = session;
        _player = _session.Player;
        _enemy = enemy;
        
        _combatActions = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.D1, () => ExecuteTurn(new NormalAttack()) },
            { ConsoleKey.D2, () => ExecuteTurn(new StealthAttack()) },
            { ConsoleKey.D3, () => ExecuteTurn(new MagicAttack()) },
            { ConsoleKey.I, () => _session.ChangeState(new InventoryState(_map, _session, this)) }
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
            _session.Logger.Log($"[{key}] Wrong input");
        }
    }

    private void ExecuteTurn(IAttackVisitor attackType)
    {
        var weapon = _player.Equipment.TryGetValue(EquipmentSlotType.MainHand, out var w) && w != null ? w : _fists;
        
        int playerDamage = weapon.AcceptDamage(attackType, _player);
        playerDamage += CritHitBonus(playerDamage, _player.GetTotalAttribute(AttributesType.Luck), 0.05);
        int playerDefense = weapon.AcceptDefense(attackType, _player);
        
        int damageToEnemy = Math.Max(0, playerDamage - _enemy.Defense);
        _enemy.Hp -= damageToEnemy;
        _session.Logger.Log($"You hit {_enemy.Name} for {damageToEnemy} dmg!");
        
        if (_enemy.Hp <= 0)
        {
            _session.Logger.Log($"{_enemy.Name} has been defeated!");
            _enemy.Die();
            _map.Characters.Remove(_enemy.Position);
            _session.ChangeState(new ExplorationState(_map, _session));
            return;
        }
        
        int damageToPlayer = Math.Max(0, _enemy.Attack - playerDefense);
        damageToPlayer += CritHitBonus(_enemy.Attack, 50, 0.10);
        _player.Hp -= damageToPlayer; 
        _session.Logger.Log($"{_enemy.Name} hits you for {damageToPlayer} dmg!");
        
        if (_player.Hp <= 0)
        {
            _session.Logger.Log("YOU DIED!");
            _session.ChangeState(new GameOverState(_session)); 
        }
    }

    public void Update() { }

    public List<string> GetAvailableActions()
    {
        return new List<string>
        {
            "  ==== COMBAT! ====",
            "  [1] Normal Attack",
            "  [2] Stealth Attack",
            "  [3] Magic Attack",
            "  [I] Open Inventory"
        };
    }
    
    public List<(DateTime, string)> GetLogHistory() => _session.Logger.GetRecentLogs();

    public UIContext GetUIContext()
    {
        _uiContext.message = _enemy.Name;
        _uiContext.barCurrent = _enemy.Hp;
        _uiContext.barMax = _enemy.MaxHp;
        
        return _uiContext;
    }

    private int CritHitBonus(int damage, int chance, double bonusPercentage)
    {
        int ret = 0;
        
        int roll = Random.Next(0, 100);
        if (roll <= chance)
        {
            ret = (int)Math.Floor(damage * bonusPercentage);
        }

        return ret;
    }
}