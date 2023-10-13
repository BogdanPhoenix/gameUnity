using System.Collections.Generic;

namespace Observer
{
    public class EventManager
    {
        private readonly Dictionary<TypeActive, IList<IEventListener>> Listeners = new();
        
        public EventManager(params TypeActive[] types)
        {
            foreach (var type in types)
            {
                Listeners.Add(type, new List<IEventListener>());
            }
        }

        public void Subscribe(TypeActive type, IEventListener listener)
        {
            var events = GetEventListeners(type);
            events.Add(listener);
        }

        public void Unsubscribe(TypeActive type, IEventListener listener)
        {
            var events = GetEventListeners(type);
            events.Remove(listener);
        }

        public void Notify(TypeActive type)
        {
            var events = GetEventListeners(type);
            foreach (var listener in events)
            {
                listener.Update();
            }
        }

        private IList<IEventListener> GetEventListeners(TypeActive type)
        {
            return Listeners[type];
        }
    }
}