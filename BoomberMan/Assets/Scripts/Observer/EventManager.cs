using System.Collections.Generic;

namespace Observer.Bomb
{
    public class EventManager
    {
        private readonly Dictionary<TypeActive, List<EventListener>> Listeners = new();
        
        public EventManager(params TypeActive[] types)
        {
            foreach (var type in types)
            {
                Listeners.Add(type, new List<EventListener>());
            }
        }

        public void Subscribe(TypeActive type, EventListener listener)
        {
            var events = GetEventListeners(type);
            events.Add(listener);
        }

        public void Unsubscribe(TypeActive type, EventListener listener)
        {
            var events = GetEventListeners(type);
            events.Remove(listener);
        }

        public void Notify(TypeActive type)
        {
            var events = GetEventListeners(type);
            foreach (var listener in events)
            {
                listener.update();
            }
        }

        private List<EventListener> GetEventListeners(TypeActive type)
        {
            return Listeners[type];
        }
    }
}