using Enum;
using Observer.Event.Interface;
using Observer.Manager.Interface;
using UnityEngine;

namespace Observer.Manager
{
    public class EventManagerGameOver : EventManager<IEventListenerGameOver>, INotifySimple
    {
        public EventManagerGameOver(params TypeActive[] types) : base(types)
        {
        }

        public void Notify(TypeActive type)
        {
            var events = GetEventListeners(type);
            foreach (var listener in events)
            {
                listener.CallGameOver();
            }
        }
    }
}