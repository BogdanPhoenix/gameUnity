using Enum;
using Observer.Event.Interface;
using Observer.Manager.Interface;

namespace Observer.Manager
{
    public class EventManagerVictory : EventManager<IEventListenerVictory>, INotifySimple
    {
        public EventManagerVictory(params TypeActive[] types) : base(types)
        {
        }

        public void Notify(TypeActive type)
        {
            var events = GetEventListeners(type);
            foreach (var listener in events)
            {
                listener.Victory();
            }
        }
    }
}