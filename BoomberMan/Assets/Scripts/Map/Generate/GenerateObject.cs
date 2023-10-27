using Enum;
using Observer.Event.Interface;
using UnityEngine;

namespace Map.Generate
{
    public abstract class GenerateObject : IEventListenerGenerateObject
    {
        protected TypeObject[,] Field;
        protected Vector2Int MapSize;
        protected readonly GenerateMapProperties MapProperties;
        protected readonly Vector2 StartPositionGenerateMap;
        protected readonly Vector2 StartPositionBomberMan;
        
        protected GenerateObject()
        {
            MapProperties = GenerateMapProperties.GetInstance();
            StartPositionGenerateMap = MapProperties.GetStartPositionGenerateMap();
            StartPositionBomberMan = MapProperties.GetStartPositionBomberMan();
        }
        
        public virtual void CreateObjects(TypeObject[,] field)
        {
            Field = field;
            MapSize = new Vector2Int(Field.GetLength(0), Field.GetLength(1));
            Generate();
        }

        protected abstract void Generate();
    }
}