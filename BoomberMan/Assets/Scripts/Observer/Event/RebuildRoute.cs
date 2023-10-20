namespace Observer.Bomb.Event
{
    public class RebuildRoute : EventListener
    {
        public void update()
        {
            Enemy.RebuildRoute();
        }
    }
}