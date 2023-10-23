using System.Collections.Generic;
using Enum;

namespace Observer.Manager
{
    public abstract class EventManager<T>
    {
        private readonly Dictionary<TypeActive, IList<T>> Listeners = new();

        protected EventManager(params TypeActive[] types)
        {
            foreach (var type in types) Listeners.Add(type, new List<T>());
        }

        public void Subscribe(TypeActive type, T listener)
        {
            var events = GetEventListeners(type);
            events.Add(listener);
        }

        public void Unsubscribe(TypeActive type, T listener)
        {
            var events = GetEventListeners(type);
            events.Remove(listener);
        }

        protected IList<T> GetEventListeners(TypeActive type)
        {
            return Listeners[type];
        }
    }
}