using GenerateField.Command;
using UnityEngine;

namespace GenerateField
{
    public class GenerateStoneBlocks
    {
        private readonly GameObject StoneObject;
        private readonly Vector2 StartPosition; 
        private Vector2Int MapSize;
        private GameObject[,] Field;

        public GenerateStoneBlocks(Vector2 startPosition, GameObject stoneObject)
        {
            StartPosition = startPosition;
            StoneObject = stoneObject;
            GenerateBoard.SetStoneObject(stoneObject);
        }

        public void CreateStoneWall(GameObject[,] field, Vector2Int mapSize)
        {
            MapSize = mapSize;
            Field = field;

            GenerateStones();
        }
        
        private void GenerateStones()
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
                    if(Field[i,j] != null) continue;

                    Field[i, j] = Object.Instantiate(StoneObject, new Vector2(StartPosition.x + i, StartPosition.y - j), StoneObject.transform.rotation);
                }
            }
        }
    }
}