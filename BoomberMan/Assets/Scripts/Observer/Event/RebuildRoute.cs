namespace Observer.Event
{
    public class RebuildRoute : IEventListener
    {
        public void Update()
        {
            BehaviorEnemy.RebuildRoute();
        }
    }
}