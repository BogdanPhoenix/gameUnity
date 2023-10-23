using Enum;
using UnityEngine;

namespace Map.Stone.Axis
{
    public class GenerateBoardAxisY : GenerateBoard
    {
        public GenerateBoardAxisY(TypeObject[,] field) : base(field)
        {
        }

        public override void GenerateAxis(Vector2Int mapSize, Vector2 startPosition)
        {
            var lastLine = mapSize.x - 1;
            var endPosition = GetEndPoint(mapSize, startPosition);

            GenerateAxisLine(mapSize.y, FirstLine, startPosition);
            GenerateAxisLine(mapSize.y, lastLine, new Vector2(endPosition.x, startPosition.y));
        }

        protected override bool CheckAddObject(int currentIndex, int indexLine)
        {
            return Field[indexLine, currentIndex] == TypeObject.None;
        }

        protected override Vector2 GetPositionObject(int currentIndex, Vector2 position)
        {
            return new Vector2(position.x, position.y - currentIndex);
        }

        protected override void Instantiate(int currentIndex, int indexLine, Vector2 position)
        {
            Object.Instantiate(Stone, position, Stone.transform.rotation);
            Field[indexLine, currentIndex] = TypeObject.Stone;
        }
    }
}