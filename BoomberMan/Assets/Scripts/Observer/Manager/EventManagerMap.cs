using Enum;
using Observer.Event.Interface;
using Observer.Manager.Interface;
using UnityEngine;

namespace Observer.Manager
{
    public class EventManagerMap : EventManager<IEventListenerMap>, INotifyMap
    {
        public EventManagerMap(params TypeActive[] types) : base(types)
        {
        }
        
        public void Notify(TypeActive type, Vector2 positionOnMap)
        {
            var events = GetEventListeners(type);
            foreach (var listener in events) listener.Update(positionOnMap);
        }
    }
}