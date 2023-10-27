using System.Collections.Generic;
using Map.ActionsOnObjects;
using Observer.Event.Interface;
using UnityEngine;

namespace Observer.Event.Map
{
    public class PowerUpOnMap : IEventListenerMap, IActionAddOnMap, INextLevel
    {
        private static PowerUpOnMap _powerUpOnMap;
        private readonly IDictionary<Vector2, GameObject> ElementsToMap = new Dictionary<Vector2, GameObject>();
        
        private PowerUpOnMap(){}
        
        public static PowerUpOnMap GetInstance()
        {
            return _powerUpOnMap ??= new PowerUpOnMap();
        }
        
        public void Update(Vector2 positionOnMap)
        {
            foreach (var (key, powerUp) in ElementsToMap)
            {
                if (!key.Equals(positionOnMap)) continue;

                Object.Instantiate(powerUp, positionOnMap, powerUp.transform.rotation);
                ElementsToMap.Remove(key);
                break;
            }
        }

        public void Add(Vector2 positionOnMap, GameObject obj)
        {
            ElementsToMap.Add(positionOnMap, obj);
        }

        public void Reset()
        {
            ElementsToMap.Clear();
        }
    }
}