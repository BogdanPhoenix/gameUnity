using System.Collections.Generic;
using System.Linq;
using Enum;
using Map.Bomb;
using Map.Enemy;
using UnityEngine;

namespace Map.Generate
{
    public class GenerateMapProperties
    {
        private static readonly Vector2Int MapSize = new(31, 31);

        private static GenerateMapProperties _properties;
        
        private readonly Vector2 StartPositionGenerateMap;
        private readonly Vector2 StartPositionBomberMan;
        private int Level;
        private TypeObject[,] Field;

        public GameObject BomberManPrefab { get; private set; }
        public GameObject GameCamera { get; private set; }
        public GameObject StonePrefab { get; private set; }
        public GameObject BrickPrefab { get; private set; }
        public GameObject EnemyPrefab { get; private set; }
        public IList<GameObject> PowerUps { get; private set; }

        private GenerateMapProperties()
        {
            BomberManPrefab = Resources.Load<GameObject>("BomberMan");
            StonePrefab = Resources.Load<GameObject>("StoneBlock");
            BrickPrefab = Resources.Load<GameObject>("Brick");
            EnemyPrefab = Resources.Load<GameObject>("Enemy");
            GameCamera = Resources.Load<GameObject>("GameCamera");
            PowerUps = ConvertArrayToList(Resources.LoadAll("PowerUp", typeof(GameObject)));
            
            StartPositionBomberMan = BomberManPrefab.transform.position;
            StartPositionGenerateMap = new Vector2(StartPositionBomberMan.x - 1, StartPositionBomberMan.y + 1);
            ResetLevel();
        }
        
        public void ResetLevel()
        {
            Level = 0;
            EnemyOnMap.GetInstance().Reset();
            BombOnMap.GetInstance().Reset();
        }
        
        private static IList<GameObject> ConvertArrayToList(IReadOnlyCollection<Object> arr)
        {
            var prefabList = new List<GameObject>(arr.Count);
            prefabList.AddRange(arr.Select(prefab => prefab as GameObject));

            return prefabList;
        }

        public static GenerateMapProperties GetInstance()
        {
            return _properties ??= new GenerateMapProperties();
        }

        public Vector2 GetStartPositionGenerateMap()
        {
            return StartPositionGenerateMap;
        }

        public Vector2 GetStartPositionBomberMan()
        {
            return StartPositionBomberMan;
        }

        public int GetCurrentLevel()
        {
            return Level;
        }

        public void NextLevel()
        {
            ++Level;
        }

        public TypeObject[,] GetDefaultField()
        {
            Field = new TypeObject[MapSize.x, MapSize.y];
            FillArray2d(Field, TypeObject.None);

            return Field;
        }
        
        private static void FillArray2d(TypeObject[,] field, TypeObject value)
        {
            var rows = field.GetLength(0);
            var cols = field.GetLength(1);

            for (var i = 0; i < rows; i++)
            for (var j = 0; j < cols; j++)
                field[i, j] = value;
        }
    }
}