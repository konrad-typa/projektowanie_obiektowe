using Erpeg.Core.Interfaces;
using Erpeg.Data.Events;
using Erpeg.Data.Models.Characters;
using Erpeg.Data.Models.Items;
using Erpeg.Data.Models.Maps;
using Erpeg.Systems;
using Erpeg.Systems.EventSystems;
using Erpeg.Systems.LogSystem;

namespace Erpeg.Systems.CharacterSystems;

public class PickUpCommand(MapData map, PlayerData player) : ICommand
{
    public void Execute()
    {
        if (map.Items.TryGetValue(player.Position, out Item item))
        {
            item.OnPickedUp(player, map);

            int range = item.NoiseRange;
            if (range > 0)
            {
                EventManager.SoundSystem.NotifyObservers(new SoundEvent
                {
                    SourceX = player.Position.x,
                    SourceY = player.Position.y,
                    Range = range,
                    SourceName = item.Name,
                    Map = map
                });
            }
        }
        else
        {
            GameLogger.Instance.Log("There is nothing to pick up here.");
        }
    }
}