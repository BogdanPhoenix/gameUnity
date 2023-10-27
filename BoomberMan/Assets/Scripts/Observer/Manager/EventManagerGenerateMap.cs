using Enum;
using Observer.Event.Interface;
using Observer.Manager.Interface;

namespace Observer.Manager
{
    public class EventManagerGenerateMap : EventManager<IEventListenerGenerateMap>, INotifySimple
    {
        public EventManagerGenerateMap(params TypeActive[] types) : base(types)
        {
        }

        public void Notify(TypeActive type)
        {
            var events = GetEventListeners(type);
            foreach (var listener in events)
            {
                listener.GenerateMap();
            }
        }
    }
}