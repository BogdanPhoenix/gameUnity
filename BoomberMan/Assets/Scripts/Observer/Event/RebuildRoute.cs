using Map.Enemy;

namespace Observer.Event
{
    public class RebuildRoute : IEventListener
    {
        private readonly EnemyOnMap EnemyOnMap = EnemyOnMap.GetInstance();

        public void Update()
        {
            EnemyOnMap.RebuildRoute();
        }
    }
}