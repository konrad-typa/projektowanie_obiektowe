using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.GameStates;
using Erpeg.Core.Interfaces;
using Erpeg.Data.Events;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Characters;

public class EnemyData(
    string name,
    (int x, int y) position,
    int attack,
    int defense,
    string species,
    ISubject<SoundEvent> soundSystem,
    ISubject<EnemyDeathEvent> speciesSystem,
    int maxhp = 200,
    int hp = 200)
    : CharacterData(name, position, maxhp, hp), Core.Interfaces.IObserver<SoundEvent>,
        Core.Interfaces.IObserver<EnemyDeathEvent>
{
    public int Attack { get; protected set; } = attack;
    public int Defense { get; protected set; } = defense;
    public string Species { get; protected set; } = species;
    private readonly ISubject<SoundEvent> _soundSystem = soundSystem;
    private readonly ISubject<EnemyDeathEvent> _speciesSystem = speciesSystem;

    public override void Interact(PlayerData player, MapData map)
    {
        GameStateManager.ChangeState(new CombatState(map, player, this));
    }
    
    // metody obserwatora

    public void OnNotify(SoundEvent eventData)
    {
        // bfs

        bool hears = true; // tymczasowe
        int distance = 3; // tymczasowe
        
        if (hears)
        {
            GameLogger.Instance.Log($"[{Name} ({Species}) on ({Position.x}, {Position.y})] " +
                                    $"heard: {eventData.SourceName} from {distance} tiles");
        }
    }
    
    public void OnNotify(EnemyDeathEvent eventData)
    {
        if (eventData.Species == Species)
        {
            if (Species == "Cowardly")
            {
                Attack /= 2;
                Defense /= 2;
            }
            else if (Species == "Aggressive")
            {
                Attack = (int)(Attack * 1.2);
                Defense = (int)(Defense * 1.2);
            }
        }
    }
    
    public void Die()
    {
        _speciesSystem.NotifyObservers(new EnemyDeathEvent { Species = Species });
        _soundSystem.RemoveObserver(this);
        _speciesSystem.RemoveObserver(this);
    }
}