using Enum;
using Observer.Event.Interface;
using Observer.Manager.Interface;

namespace Observer.Manager
{
    public class EventManagerSimple : EventManager<IEventListenerButton>, INotifySimple
    {
        public EventManagerSimple(params TypeActive[] types) : base(types)
        {
        }

        public void Notify(TypeActive type)
        {
            var events = GetEventListeners(type);
            foreach (var listener in events) listener.Update();
        }
    }
}