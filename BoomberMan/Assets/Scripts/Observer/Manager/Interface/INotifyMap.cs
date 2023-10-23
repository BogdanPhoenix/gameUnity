using UnityEngine;
using Enum;

namespace Observer.Manager.Interface
{
    public interface INotifyMap
    {
        void Notify(TypeActive type, Vector2 positionOnMap);
    }
}