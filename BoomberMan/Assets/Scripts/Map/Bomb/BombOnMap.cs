using System.Linq;
using Map.ActionsOnObjects;

namespace Map.Bomb
{
    public class BombOnMap : ObjectOnMap<BombObject>
    {
        private static ObjectOnMap<BombObject> _objectOnMap;

        private BombOnMap() {}

        public static ObjectOnMap<BombObject> GetInstance()
        {
            return _objectOnMap ??= new BombOnMap();
        }

        public override void Active()
        {
            for (var i = 0; i < Object.Count; ++i)
            {
                Object.ElementAt(i).Explosion();
            }
        }
    }
}