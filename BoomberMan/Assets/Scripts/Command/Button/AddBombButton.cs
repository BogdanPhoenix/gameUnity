using BomberMan;
using Enum;
using Map.ActionsOnObjects;
using Map.Bomb;
using Observer.Event;
using Observer.Event.Interface;
using Observer.Manager;
using Observer.Manager.Interface;
using UnityEngine;

namespace Command.Button
{
    public class AddBombButton : ButtonActiveCommand
    {
        private readonly BomberManPlayer BomberMan;
        private readonly GameObject BombPrefab;
        private readonly EventManager<IEventListenerButton> Manager;
        private readonly IActionAdd<BombObject> Bombs;

        public AddBombButton(KeyCode addBombButton, GameObject bombPrefab) : base(addBombButton)
        {
            BombPrefab = bombPrefab;
            BomberMan = Object.FindObjectOfType<BomberManPlayer>();
            Bombs = BombOnMap.GetInstance();

            Manager = new EventManagerSimple(TypeActive.EnemyRebuildRoute);
            Manager.Subscribe(TypeActive.EnemyRebuildRoute, new RebuildRoute());
        }

        public override void Execute()
        {
            if (!Check()) return;

            var obj = Object.Instantiate(BombPrefab, new Vector2(Mathf.Round(BomberMan.transform.position.x),
                Mathf.Round(BomberMan.transform.position.y)), BomberMan.transform.rotation);
            Bombs.Add(obj);

            ((INotifySimple)Manager).Notify(TypeActive.EnemyRebuildRoute);
        }

        private bool Check()
        {
            var size = new Vector2(BomberMan.sensorSize, BomberMan.sensorSize);

            return CheckKeyDown() &&
                   Bombs.GetCount() < BomberMan.Power.BombsAllowed &&
                   !CheckLayer(size, BomberMan.bombLayer) &&
                   !CheckLayer(size, BomberMan.fireLayer) &&
                   !CheckLayer(size, BomberMan.brickLayer);

        }

        private bool CheckLayer(Vector2 size, LayerMask layer)
        {
            return Physics2D.OverlapBox(BomberMan.sensor.position, size, 0, layer);
        }
    }
}