using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Map.PowerUp
{
    public class GeneratePowerUpBlocks : GenerateObject
    {
        private const string BrickName = "Brick(Clone)";
        private const int PowerMultiplier = 2;
        private static IDictionary<Vector2, GameObject> _elementsToMap;
        
        private readonly IList<GameObject> Elements;
        private readonly GameObject BomberMan;
        private readonly IList<PowerUpElement> PowerUpElements;
        
        public GeneratePowerUpBlocks(IList<GameObject> elements, GameObject bomberMan)
        {
            Elements = elements;
            BomberMan = bomberMan;
            PowerUpElements = new List<PowerUpElement>();

            foreach (var element in elements)
            {
                PowerUpElements.Add(element.GetComponent<PowerUpElement>());
            }
        }
        
        public override void CreateObjects(TypeObject[,] field)
        {
            _elementsToMap = new Dictionary<Vector2, GameObject>();
            base.CreateObjects(field);
        }
        
        protected override void Generate()
        {
            var countPower = 0;

            while (countPower < GenerateMap.GetLevel() * PowerMultiplier)
            {
                var positionToArray = new Vector2Int(
                    Random.Range(0, MapSize.x),
                    Random.Range(0, MapSize.y));

                var positionToMap = new Vector2(GenerateMap.GetStartPosition().x + positionToArray.x, GenerateMap.GetStartPosition().y - positionToArray.y);
                
                if(!CheckSpacePresence(positionToArray) || !CheckPositionAdd(positionToMap)) 
                    continue;
                
                Debug.Log(positionToMap);
                
                var element = Elements[ChooseRandomElementIndex()];
                
                _elementsToMap.Add(positionToMap, element);
                
                ++countPower;
            }
        }
        
        private bool CheckSpacePresence(Vector2Int positionToArray)
        {
            return Field[positionToArray.x, positionToArray.y] != TypeObject.None && Field[positionToArray.x, positionToArray.y] == TypeObject.Brick;
        }
        
        private bool CheckPositionAdd(Vector2 position)
        {
            return Vector2.Distance(BomberMan.transform.position, position) > 1.5f;
        }
        
        private int ChooseRandomElementIndex()
        {
            var totalProbability = PowerUpElements.Aggregate<PowerUpElement, float>(0, (current, element) => current + element.Weight);
            var randomValue = Random.Range(0f, totalProbability);
            var cumulativeProbability = 0f;
            
            for (var i = 0; i < PowerUpElements.Count; i++)
            {
                cumulativeProbability += PowerUpElements[i].Weight;

                if (randomValue <= cumulativeProbability)
                {
                    return i;
                }
            }
            return 0;
        }
        
        public static void AddPowerUpToMap(Vector2 positionOnMap)
        {
            foreach (var (key, powerUp) in _elementsToMap)
            {
                if (!key.Equals(positionOnMap)) continue;

                Object.Instantiate(powerUp, positionOnMap, powerUp.transform.rotation);
                _elementsToMap.Remove(key);
                break;
            }
        }
    }
}