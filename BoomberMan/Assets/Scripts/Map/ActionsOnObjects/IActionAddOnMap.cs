using UnityEngine;

namespace Map.ActionsOnObjects
{
    public interface IActionAddOnMap
    {
        void Add(Vector2 positionOnMap, GameObject obj);
    }
}