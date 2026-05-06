using Erpeg.Data.Events;
using Erpeg.Systems; 

namespace Erpeg.Systems.EventSystems
{
    public static class EventManager
    {
        public static EventBus<SoundEvent> SoundSystem { get; private set; }
        public static EventBus<EnemyDeathEvent> SpeciesSystem { get; private set; }
        
        public static void Initialize()
        {
            SoundSystem = new EventBus<SoundEvent>();
            SpeciesSystem = new EventBus<EnemyDeathEvent>();
        }
    }
}