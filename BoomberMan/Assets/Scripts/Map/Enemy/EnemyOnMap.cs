using System.Collections.Generic;
using UnityEngine;

namespace Map.Enemy
{
    public class EnemyOnMap
    {
        private static EnemyOnMap _enemyOnMap;
        private readonly ISet<BehaviorEnemy> Enemies;

        private EnemyOnMap()
        {
            Enemies = new HashSet<BehaviorEnemy>();
        }

        public static EnemyOnMap GetInstance()
        {
            return _enemyOnMap ??= new EnemyOnMap();
        }
        
        public void AddEnemy(GameObject enemy)
        {
            Enemies.Add(enemy.GetComponent<BehaviorEnemy>());
        }

        public void RemoveEnemy(BehaviorEnemy behaviorEnemy)
        {
            Enemies.Remove(behaviorEnemy);
        
            if (Enemies.Count != 0) return;
        
            // FindObjectOfType<GenerateMap>().NextLevel();
        }
    
        public void RebuildRoute()
        {
            foreach (var item in Enemies)
            {
                item.ReCalculateNextStep();
            }
        }
    }
}