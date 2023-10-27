using Enum;
using Map.Generate;
using Map.Stone.Axis;
using UnityEngine;

namespace Map.Stone
{
    public class GenerateStoneBlocks : GenerateObject
    {
        private readonly GameObject StoneObject;

        public GenerateStoneBlocks(GameObject stoneObject)
        {
            StoneObject = stoneObject;
            GenerateBoard.SetStoneObject(stoneObject);
        }

        protected override void Generate()
        {
            var axisX = new GenerateBoardAxisX(Field);
            var axisY = new GenerateBoardAxisY(Field);

            axisX.GenerateAxis(MapSize, StartPositionGenerateMap);
            axisY.GenerateAxis(MapSize, StartPositionGenerateMap);

            GenerateInside();
        }

        private void GenerateInside()
        {
            for (var i = 2; i < MapSize.x; i += 2)
            for (var j = 2; j < MapSize.y; j += 2)
            {
                if (Field[i, j] != TypeObject.None) continue;

                Object.Instantiate(StoneObject, new Vector2(StartPositionGenerateMap.x + i, StartPositionGenerateMap.y - j),
                    StoneObject.transform.rotation);
                Field[i, j] = TypeObject.Stone;
            }
        }
    }
}