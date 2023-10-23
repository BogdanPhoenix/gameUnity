using System.Collections.Generic;
using UnityEngine;

namespace Map.ActionsOnObjects
{
    public abstract class ObjectOnMap<T> : IActionAdd<T>, IActionRemove<T>, IActionActive<T>
    {
        protected readonly ISet<T> Object = new HashSet<T>();
        
        public void Add(GameObject obj)
        {
            Object.Add(obj.GetComponent<T>());
        }

        public int GetCount()
        {
            return Object.Count;
        }

        public void Remove(T enemyObject)
        {
            Object.Remove(enemyObject);
        }

        public abstract void Active();
    }
}