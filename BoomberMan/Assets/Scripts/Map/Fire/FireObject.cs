using Enum;
using Observer.Event.Interface;
using Observer.Event.Map;
using Observer.Manager;
using Observer.Manager.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace Map.Fire
{
    public class FireObject : MonoBehaviour
    {
        private const string TagBrick = "Brick";

        private EventManager<IEventListenerMap> EventManager;
    
        [FormerlySerializedAs("BrickDeathEffect")]
        public GameObject brickDeathEffect;

        private void Start()
        {
            EventManager = new EventManagerMap(TypeActive.UpdateMap);
            EventManager.Subscribe(TypeActive.UpdateMap, PowerUpOnMap.GetInstance());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag(TagBrick)) return;
            Destroy(other.gameObject);
            Instantiate(brickDeathEffect, transform.position, transform.rotation);
        
            ((INotifyMap)EventManager).Notify(TypeActive.UpdateMap, other.gameObject.transform.position);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}