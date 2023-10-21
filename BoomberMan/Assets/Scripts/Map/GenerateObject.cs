using UnityEngine;

namespace Map
{
    public abstract class GenerateObject
    {
        protected Vector2Int MapSize;
        protected TypeObject[,] Field;
        
        public virtual void CreateObjects(TypeObject[,] field)
        {
            Field = field;
            MapSize = new Vector2Int(Field.GetLength(0), Field.GetLength(1));
            Generate();
        }
        
        protected abstract void Generate();
    }
}