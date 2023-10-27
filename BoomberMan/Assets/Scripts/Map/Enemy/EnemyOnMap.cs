using Enum;
using Map.ActionsOnObjects;
using Observer.Event.Interface;
using Observer.Manager;
using Observer.Manager.Interface;

namespace Map.Enemy
{
    public class EnemyOnMap : ObjectOnMap<EnemyObject>
    {
        private static ObjectOnMap<EnemyObject> _emptyOnMap;

        private readonly EventManager<IEventListenerVictory> EventVictory;

        private EnemyOnMap()
        {
            EventVictory = new EventManagerVictory(TypeActive.Victory);
            EventVictory.Subscribe(TypeActive.Victory, new LevelVictory());
        }

        public static ObjectOnMap<EnemyObject> GetInstance()
        {
            return _emptyOnMap ??= new EnemyOnMap();
        }

        public override void Remove(EnemyObject enemyObject)
        {
            base.Remove(enemyObject);
            
            if(Object.Count > 0) return;
            
            ((INotifySimple)EventVictory).Notify(TypeActive.Victory);
        }
        
        public override void Active()
        {
            foreach (var item in Object) item.ReCalculateNextStep();
        }
    }
}