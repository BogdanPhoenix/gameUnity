using UnityEngine;

namespace Map.ActionsOnObjects
{
    public interface IActionAdd<T>
    {
        void Add(GameObject obj);
        int GetCount();
    }
}