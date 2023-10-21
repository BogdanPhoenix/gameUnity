using Map.Stone.Axis;
using UnityEngine;

namespace Map.Stone
{
    public class GenerateStoneBlocks : GenerateObject
    {
        private readonly GameObject StoneObject;
        private readonly Vector2 StartPosition;

        public GenerateStoneBlocks(GameObject stoneObject)
        {
            StoneObject = stoneObject;
            StartPosition = GenerateMap.GetStartPosition();
            GenerateBoard.SetStoneObject(stoneObject);
        }
        
        protected override void Generate()
        {
            var axisX = new GenerateBoardAxisX(Field);
            var axisY = new GenerateBoardAxisY(Field);
            
            axisX.GenerateAxis(MapSize, StartPosition);
            axisY.GenerateAxis(MapSize, StartPosition);
            
            GenerateInside();
        }

        private void GenerateInside()
        {
            for (var i = 2; i < MapSize.x; i += 2)
            {
                for (var j = 2; j < MapSize.y; j += 2)
                {
                    if(Field[i,j] != TypeObject.None) continue;

                    Object.Instantiate(StoneObject, new Vector2(StartPosition.x + i, StartPosition.y - j), StoneObject.transform.rotation);
                    Field[i, j] = TypeObject.Stone;
                }
            }
        }
    }
}