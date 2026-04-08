using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.GameStates;

namespace Erpeg.Data.Models.Characters;

public class EnemyData(string name, (int x, int y) position, int attack, int defense, int maxhp = 200, int hp = 200)
    : CharacterData(name, position, maxhp, hp)
{
    public int Attack { get; protected set; } = attack;
    public int Defense { get; protected set; } = defense;
    
    public override void Interact(PlayerData player, MapData map)
    {
        GameStateManager.ChangeState(new CombatState(map, player, this));
    }
}