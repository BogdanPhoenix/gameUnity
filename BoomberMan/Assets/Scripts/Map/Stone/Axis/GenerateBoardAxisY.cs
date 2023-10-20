using UnityEngine;

namespace Map.Stone.Axis
{
    public class GenerateBoardAxisY : GenerateBoard
    {
        public GenerateBoardAxisY(GameObject[,] field) : base(field) {}
        
        public override void GenerateAxis(Vector2Int mapSize, Vector2 startPosition)
        {
            var lastLine = mapSize.x - 1;
            var endPosition = GetEndPoint(mapSize, startPosition);
            
            GenerateAxisLine(mapSize.y, FirstLine, startPosition);
            GenerateAxisLine(mapSize.y, lastLine, new Vector2(endPosition.x, startPosition.y));
        }

        protected override bool CheckAddObject(int currentIndex, int indexLine)
        {
            return Field[indexLine, currentIndex] == null;
        }

        protected override Vector2 GetPositionObject(int currentIndex, Vector2 position)
        {
            return new Vector2(position.x, position.y - currentIndex);
        }

        protected override void Instantiate(int currentIndex, int indexLine, Vector2 position)
        {
            Field[indexLine, currentIndex] = Object.Instantiate(Stone, position, Stone.transform.rotation);
        }

    }
}