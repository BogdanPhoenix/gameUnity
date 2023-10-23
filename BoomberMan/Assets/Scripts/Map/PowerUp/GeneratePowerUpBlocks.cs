using System.Collections.Generic;
using System.Linq;
using Enum;
using Map.ActionsOnObjects;
using Observer.Event.Map;
using UnityEngine;

namespace Map.PowerUp
{
    public class GeneratePowerUpBlocks : GenerateObject
    {
        private const int PowerMultiplier = 2;

        private readonly IList<GameObject> Elements;
        private readonly IList<PowerUpElement> PowerUpElements;

        public GeneratePowerUpBlocks(IList<GameObject> elements)
        {
            Elements = elements;
            PowerUpElements = new List<PowerUpElement>();

            foreach (var element in elements) PowerUpElements.Add(element.GetComponent<PowerUpElement>());
        }

        public override void CreateObjects(TypeObject[,] field)
        {
            INexlLevel objectToMap = PowerUpOnMap.GetInstance();
            objectToMap.Reset();
            
            base.CreateObjects(field);
        }

        protected override void Generate()
        {
            var countPower = 0;
            IActionAddOnMap objectsToMap = PowerUpOnMap.GetInstance();

            while (countPower < GenerateMap.GetLevel() * PowerMultiplier)
            {
                var positionToArray = new Vector2Int(
                    Random.Range(0, MapSize.x),
                    Random.Range(0, MapSize.y));

                var positionToMap = new Vector2(GenerateMap.GetStartPosition().x + positionToArray.x,
                    GenerateMap.GetStartPosition().y - positionToArray.y);

                if (!CheckSpacePresence(positionToArray) || !CheckPositionAdd(positionToMap))
                    continue;

                Debug.Log(positionToMap);

                var element = Elements[ChooseRandomElementIndex()];
                objectsToMap.Add(positionToMap, element);

                ++countPower;
            }
        }

        private bool CheckSpacePresence(Vector2Int positionToArray)
        {
            return Field[positionToArray.x, positionToArray.y] != TypeObject.None &&
                   Field[positionToArray.x, positionToArray.y] == TypeObject.Brick;
        }

        private bool CheckPositionAdd(Vector2 position)
        {
            return Vector2.Distance(BomberMan.transform.position, position) > 1.5f;
        }

        private int ChooseRandomElementIndex()
        {
            var totalProbability =
                PowerUpElements.Aggregate<PowerUpElement, float>(0, (current, element) => current + element.Weight);
            var randomValue = Random.Range(0f, totalProbability);
            var cumulativeProbability = 0f;

            for (var i = 0; i < PowerUpElements.Count; i++)
            {
                cumulativeProbability += PowerUpElements[i].Weight;

                if (randomValue <= cumulativeProbability) return i;
            }

            return 0;
        }
    }
}