using Map.ActionsOnObjects;

namespace Map.Enemy
{
    public class EnemyOnMap : ObjectOnMap<EnemyObject>
    {
        private static ObjectOnMap<EnemyObject> _emptyOnMap;

        private EnemyOnMap() {}

        public static ObjectOnMap<EnemyObject> GetInstance()
        {
            return _emptyOnMap ??= new EnemyOnMap();
        }
        
        public override void Active()
        {
            foreach (var item in Object) item.ReCalculateNextStep();
        }
    }
}