using BomberMan;
using Observer.Event.Interface;
using Observer.Manager;
using UnityEngine;

namespace Map.Generate
{
    public class GenerateMap : MonoBehaviour
    {
        private GenerateMapProperties Properties;
        private EventManager<IEventListenerGenerateObject> CreateObjects;
        private GenerateMapBuilder Builder;
        private BomberManPower BomberManPower;

        public void Start()
        {
            Builder = GenerateMapBuilder.GetInstance();
            BomberManPower = BomberManPower.GetInstance();
            
            BomberManPower.ResetPower();
            Builder.Properties.ResetLevel();
            Builder.GenerateMap();
        }
    }
}