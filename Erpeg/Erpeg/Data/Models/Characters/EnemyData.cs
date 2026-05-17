using Erpeg.Core.StateMachine;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.GameStates;
using Erpeg.Core.Interfaces;
using Erpeg.Core.Utils;
using Erpeg.Data.Events;
using Erpeg.Systems.EventSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Characters;

public class EnemyData(
    string name,
    (int x, int y) position,
    int attack,
    int defense,
    string species,
    int maxhp = 200,
    int hp = 200)
    : CharacterData(name, position, maxhp, hp), Core.Interfaces.IObserver<SoundEvent>,
        Core.Interfaces.IObserver<EnemyDeathEvent>
{
    public int Attack { get; protected set; } = attack;
    public int Defense { get; protected set; } = defense;
    public string Species { get; protected set; } = species;
    private static readonly Random Rng = new Random();
    
    // do ruchu
    private DateTime _lastMoveTime = DateTime.Now;
    private TimeSpan _moveInterval = TimeSpan.FromSeconds(0.6 + (Rng.NextDouble() * 0.4));

    public override void Interact(PlayerData player, MapData map, InteractionCallback interactionCallback)
    {
        interactionCallback.OnCombatStart?.Invoke(this);
    }
    
    //
    public void MoveRandomly(MapData map)
    {
        if (DateTime.Now - _lastMoveTime < _moveInterval)
        {
            return;
        }
        
        var directions = new (int dx, int dy)[]
        {
            (0, -1), (0, 1), (-1, 0), (1, 0)
        };

        var validMoves = new List<(int x, int y)>();

        foreach (var dir in directions)
        {
            int newX = Position.x + dir.dx;
            int newY = Position.y + dir.dy;
            var targetPos = (newX, newY);

            if (newX >= 0 && newX < map.SizeX && newY >= 0 && newY < map.SizeY)
            {
                if (map.Layout[newX, newY] != TileType.Wall && !map.Characters.ContainsKey(targetPos))
                {
                    validMoves.Add(targetPos);
                }
            }
        }

        if (validMoves.Count > 0)
        {
            var chosenMove = validMoves[Rng.Next(validMoves.Count)];
            
            map.Characters.Remove(Position);
            Position = chosenMove;
            map.Characters[Position] = this;
        }
        
        _lastMoveTime = DateTime.Now;
        _moveInterval = TimeSpan.FromSeconds(0.4 + (Rng.NextDouble() * 0.4));
    }
    
    // metody obserwatora

    public void OnNotify(SoundEvent eventData)
    {
        bool hears = PathfindingHelper.CanHearSound(
            eventData.Map, 
            Position, 
            (eventData.SourceX, eventData.SourceY), 
            eventData.Range, 
            out int distance
        );
        
        if (hears)
        {
            eventData.FeedbackLog?.Invoke($"[{Name} on ({Position.x}, {Position.y})] " +
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
                Attack = (int)(Attack * 1.4);
                Defense = (int)(Defense * 1.4);
            }
        }
    }
    
    public void Die()
    {
        EventManager.SpeciesSystem.NotifyObservers(new EnemyDeathEvent { Species = Species });
        
        EventManager.SoundSystem.RemoveObserver(this);
        EventManager.SpeciesSystem.RemoveObserver(this);
    }
}