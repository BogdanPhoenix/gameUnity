using UnityEngine;

namespace GenerateField.Brick
{
    public class GenerateBrickBlock 
    {
        private const float SpawnThreshold = 0.5f;
        private readonly GameObject BrickObject;
        private readonly Vector2 StartPosition;
        private readonly GameObject BomberMan;
        private Vector2Int MapSize;
        private GameObject[,] Field;

        public GenerateBrickBlock(GameObject bomberMan, Vector2 startPosition, GameObject brickObject)
        {
            BomberMan = bomberMan;
            StartPosition = startPosition;
            BrickObject = brickObject;
        }

        public void CreateBrickWall(GameObject[,] field, Vector2Int mapSize)
        {
            MapSize = mapSize;
            Field = field;

            GenerateBricks();
        }

        private void GenerateBricks()
        {
            for (var i = 0; i < MapSize.x; ++i)
            {
                for (var j = 0; j < MapSize.y; ++j)
                {
                    var positionBrick = new Vector2(StartPosition.x + i, StartPosition.y - j);
                    
                    if(!CheckSpacePresence(i,j) || !CheckPositionAddBrick(positionBrick)) 
                        continue;
                    
                    Field[i, j] = Object.Instantiate(BrickObject, positionBrick, BrickObject.transform.rotation);
                }
            }
        }

        private bool CheckSpacePresence(int i, int j)
        {
            return Field[i, j] == null && Random.value < SpawnThreshold;
        }

        private bool CheckPositionAddBrick(Vector2 position)
        {
            return Vector2.Distance(BomberMan.transform.position, position) > 1.5f;
        }
    }
}