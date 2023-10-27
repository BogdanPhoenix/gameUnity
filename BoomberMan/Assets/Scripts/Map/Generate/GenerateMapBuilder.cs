using Enum;
using Map.Brick;
using Map.Enemy;
using Map.PowerUp;
using Map.Stone;
using Observer.Event.Interface;
using Observer.Manager;
using Observer.Manager.Interface;
using UnityEngine;

namespace Map.Generate
{
    public class GenerateMapBuilder : IEventListenerGenerateMap
    {
        private static GenerateMapBuilder _builder;
        private readonly EventManager<IEventListenerGenerateObject> CreateObjects;
        private TypeObject[,] Field;
        
        public readonly GenerateMapProperties Properties;

        private GenerateMapBuilder()
        {
            Properties = GenerateMapProperties.GetInstance();
            CreateObjects = new EventManagerGenerateObject(TypeActive.GenerateObjects);
            
            CreateObjects.Subscribe(TypeActive.GenerateObjects, 
                new GenerateStoneBlocks(Properties.StonePrefab),
                new GenerateBrickBlock(Properties.BrickPrefab),
                new GeneratePowerUpBlocks(Properties.PowerUps),
                new GenerateEnemy(Properties.EnemyPrefab)
            );
        }

        public static GenerateMapBuilder GetInstance()
        {
            return _builder ??= new GenerateMapBuilder();
        }

        public void GenerateMap()
        {
            ClearScene();
            SetStartPositionBomberMan();
            Properties.NextLevel();
            ((INotifyGenerateObjects)CreateObjects).Notify(TypeActive.GenerateObjects, Properties.GetDefaultField());
        }

        private void SetStartPositionBomberMan()
        {
            Object.Instantiate(Properties.BomberManPrefab, Properties.GetStartPositionBomberMan(),
                Properties.BomberManPrefab.transform.rotation);
            Object.Instantiate(Properties.GameCamera, Properties.GameCamera.transform.position,
                Properties.GameCamera.transform.rotation);
        }

        private static void ClearScene()
        {
            var allObjects = Object.FindObjectsOfType<GameObject>();

            foreach (var obj in allObjects)
            {
                if (CheckSystemObject(obj))
                {
                    Object.Destroy(obj);
                }
            }
        }

        private static bool CheckSystemObject(GameObject obj)
        {
            return !obj.CompareTag("Field") && !obj.CompareTag("UI") && !obj.CompareTag("BackgroundMusic");
        }
    }
}