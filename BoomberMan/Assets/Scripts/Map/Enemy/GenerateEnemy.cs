using Enum;
using Map.ActionsOnObjects;
using UnityEngine;

namespace Map.Enemy
{
    public class GenerateEnemy : GenerateObject
    {
        private const float MaxDistance = 4f;
        private const int PowerMultiplier = 1;
        private readonly GameObject EnemyObject;
        private readonly IActionAdd<EnemyObject> EnemiesOnMap;

        public GenerateEnemy(GameObject enemyObject)
        {
            EnemyObject = enemyObject;
            EnemiesOnMap = EnemyOnMap.GetInstance();
        }

        protected override void Generate()
        {
            var countPower = 0;

            while (countPower < GenerateMap.GetLevel() + PowerMultiplier)
            {
                var positionToArray = new Vector2Int(
                    Random.Range(0, MapSize.x),
                    Random.Range(0, MapSize.y));

                var positionToMap = new Vector2(GenerateMap.GetStartPosition().x + positionToArray.x,
                    GenerateMap.GetStartPosition().y - positionToArray.y);

                if (!(CheckSpacePresence(positionToArray) && CheckPositionAdd(positionToMap)))
                    continue;

                Debug.Log("Position in Array=" + positionToArray + "\nPosition in Map=" + positionToMap);

                var obj = Object.Instantiate(EnemyObject, positionToMap, EnemyObject.transform.rotation);
                EnemiesOnMap.Add(obj);

                ++countPower;
            }
        }

        private bool CheckSpacePresence(Vector2Int positionToArray)
        {
            return Field[positionToArray.x, positionToArray.y] == TypeObject.None;
        }

        private bool CheckPositionAdd(Vector2 position)
        {
            return Vector2.Distance(BomberMan.transform.position, position) > MaxDistance;
        }
    }
}