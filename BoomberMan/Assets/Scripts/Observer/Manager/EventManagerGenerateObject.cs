using Enum;
using Observer.Event.Interface;
using Observer.Manager.Interface;

namespace Observer.Manager
{
    public class EventManagerGenerateObject : EventManager<IEventListenerGenerateObject>, INotifyGenerateObjects
    {
        public EventManagerGenerateObject(params TypeActive[] types) : base(types)
        {
        }

        public void Notify(TypeActive type, TypeObject[,] field)
        {
            var events = GetEventListeners(type);
            foreach (var listener in events)
            {
                listener.CreateObjects(field);
            }
        }
    }
}