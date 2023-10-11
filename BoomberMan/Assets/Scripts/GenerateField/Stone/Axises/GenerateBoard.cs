using UnityEngine;

namespace GenerateField.Command
{
    public abstract class GenerateBoard
    {
        protected const int FirstLine = 0;
        protected static GameObject Stone;
        protected readonly GameObject[,] Field;

        protected GenerateBoard(GameObject[,] field)
        {
            Field = field;
        }

        public static void SetStoneObject(GameObject stone)
        {
            Stone = stone;
        }
        
        protected void GenerateAxisLine(int end, int indexLine, Vector2 positionStart)
        {
            for (var i = 0; i < end; ++i)
            {
                if(!CheckAddObject(i, indexLine)) continue;

                var position = GetPositionObject(i, positionStart);
                Instantiate(i, indexLine, position);
            }
        }

        protected Vector2 GetEndPoint(Vector2Int mapSize, Vector2 startPosition)
        {
            return new Vector2(startPosition.x + (mapSize.x - 1), startPosition.y - (mapSize.y - 1));
        }

        public abstract void GenerateAxis(Vector2Int mapSize, Vector2 startPosition);
        protected abstract bool CheckAddObject(int currentIndex, int indexLine);
        protected abstract Vector2 GetPositionObject(int currentIndex, Vector2 position);
        protected abstract void Instantiate(int currentIndex, int indexLine, Vector2 position);
    }
}