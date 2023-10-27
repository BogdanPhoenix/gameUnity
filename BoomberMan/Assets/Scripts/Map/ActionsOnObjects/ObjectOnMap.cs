using System.Collections.Generic;
using UnityEngine;

namespace Map.ActionsOnObjects
{
    public abstract class ObjectOnMap<T> : IActionAdd<T>, IActionRemove<T>, IActionActive<T>
    {
        protected ISet<T> Object;

        protected ObjectOnMap()
        {
            Reset();
        }
        
        public void Reset()
        {
            Object = new HashSet<T>();
        }
        
        public void Add(GameObject obj)
        {
            Object.Add(obj.GetComponent<T>());
        }

        public int GetCount()
        {
            return Object.Count;
        }

        public virtual void Remove(T enemyObject)
        {
            Object.Remove(enemyObject);
        }

        public abstract void Active();
    }
}