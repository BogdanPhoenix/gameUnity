using UnityEngine;

namespace Map
{
    public abstract class GenerateObject
    {
        protected Vector2Int MapSize;
        protected GameObject[,] Field;
        
        public virtual void CreateObjects(GameObject[,] field)
        {
            Field = field;
            MapSize = new Vector2Int(Field.GetLength(0), Field.GetLength(1));
            Generate();
        }
        
        protected abstract void Generate();
    }
}