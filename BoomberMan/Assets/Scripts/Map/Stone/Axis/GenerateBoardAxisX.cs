using UnityEngine;

namespace Map.Stone.Axis
{
    public class GenerateBoardAxisX : GenerateBoard
    {
        public GenerateBoardAxisX(GameObject[,] field) : base(field) {}
        
        public override void GenerateAxis(Vector2Int mapSize, Vector2 startPosition)
        {
            var lastLine = mapSize.y - 1;
            var endPosition = GetEndPoint(mapSize, startPosition);
            
            GenerateAxisLine(mapSize.x, FirstLine, startPosition);
            GenerateAxisLine(mapSize.x, lastLine, new Vector2(startPosition.x, endPosition.y));
        }

        protected override bool CheckAddObject(int currentIndex, int indexLine)
        {
            return Field[currentIndex, indexLine] == null;
        }

        protected override Vector2 GetPositionObject(int currentIndex, Vector2 position)
        {
            return new Vector2(position.x + currentIndex, position.y);
        }
        
        protected override void Instantiate(int currentIndex, int indexLine, Vector2 position)
        {
            Field[currentIndex, indexLine] = Object.Instantiate(Stone, position, Stone.transform.rotation);
        }
    }
}