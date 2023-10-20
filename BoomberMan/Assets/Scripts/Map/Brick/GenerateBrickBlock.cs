using UnityEngine;

namespace Map.Brick
{
    public class GenerateBrickBlock : GenerateObject
    {
        private const float MaxDistance = 1.5f;
        private const float SpawnThreshold = 0.5f;
        private readonly GameObject BrickObject;
        private readonly GameObject BomberMan;

        public GenerateBrickBlock(GameObject bomberMan, GameObject brickObject)
        {
            BomberMan = bomberMan;
            BrickObject = brickObject;
        }

        protected override void Generate()
        {
            var startPosition = GenerateMap.GetStartPosition();
            
            for (var i = 0; i < MapSize.x; ++i)
            {
                for (var j = 0; j < MapSize.y; ++j)
                {
                    var positionBrick = new Vector2(startPosition.x + i, startPosition.y - j);
                    
                    if(!CheckSpacePresence(i,j) || !CheckPositionAdd(positionBrick)) 
                        continue;
                    
                    Field[i, j] = Object.Instantiate(BrickObject, positionBrick, BrickObject.transform.rotation);
                }
            }
        }

        private bool CheckSpacePresence(int i, int j)
        {
            return Field[i, j] == null && Random.value < SpawnThreshold;
        }

        private bool CheckPositionAdd(Vector2 position)
        {
            return Vector2.Distance(BomberMan.transform.position, position) > MaxDistance;
        }
    }
}