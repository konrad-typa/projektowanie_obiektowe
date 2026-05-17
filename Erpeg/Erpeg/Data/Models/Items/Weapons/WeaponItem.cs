using Erpeg.Core.Interfaces;
using Erpeg.Data.Events;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems.EventSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Data.Models.Items.Weapons;

public class WeaponItem(string name, int value, 
    WeaponGripType grip, int damage, double hitspeed, int range, double weight, char symbol = '?', string color = "")
    : Item(name, value, weight, symbol, color)
{
    public WeaponGripType Grip { get; } = grip;
    public override int Damage { get; protected set; } = damage;
    public double Hitspeed { get; } = hitspeed;
    public int Range { get; } = range;
    public override bool BlocksOffHand => Grip == WeaponGripType.TwoHanded;
    
    public override void OnPickedUp(PlayerData player, MapData map, ILogger logger)
    {
        if (player.TryAddWeight(Weight))
        {
            player.Inventory.Add(this);
            map.Items.Remove(player.Position);
            logger.Log($"Picked up {Name}.");
            
            int range = NoiseRange;
            if (range > 0)
            {
                EventManager.SoundSystem.NotifyObservers(new SoundEvent
                {
                    SourceX = player.Position.x,
                    SourceY = player.Position.y,
                    Range = range,
                    SourceName = Name,
                    Map = map,
                    FeedbackLog = message => logger.Log(message),
                });
            }
        }
        else
        {
            logger.Log("Not enough space in inventory!");
        }
    }
    
    public override void Use(PlayerData player, ILogger logger)
    {
        player.EquipWeapon(this); 
        logger.Log($"Equipped {Name}.");
    }
}