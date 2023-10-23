using Enum;
using UnityEngine;

namespace Map
{
    public abstract class GenerateObject
    {
        private const string TagPlayer = "Player";
        
        protected TypeObject[,] Field;
        protected Vector2Int MapSize;
        protected readonly GameObject BomberMan = GameObject.FindGameObjectWithTag(TagPlayer);

        public virtual void CreateObjects(TypeObject[,] field)
        {
            Field = field;
            MapSize = new Vector2Int(Field.GetLength(0), Field.GetLength(1));
            Generate();
        }

        protected abstract void Generate();
    }
}