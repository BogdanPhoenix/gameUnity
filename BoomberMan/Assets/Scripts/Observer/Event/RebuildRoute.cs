using Map.ActionsOnObjects;
using Map.Enemy;
using Observer.Event.Interface;

namespace Observer.Event
{
    public class RebuildRoute : IEventListenerButton
    {
        private readonly IActionActive<EnemyObject> EnemiesOnMap = EnemyOnMap.GetInstance();

        public void Update()
        {
            EnemiesOnMap.Active();
        }
    }
}