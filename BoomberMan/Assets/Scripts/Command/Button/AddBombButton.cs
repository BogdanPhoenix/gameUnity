using BomberMan;
using ChainResponsibility.Command.Button;
using Observer;
using Observer.Event;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Command.Button
{
    public class AddBombButton : ButtonActiveCommand
    {
        private const string Tag = "Bomb";
        
        private readonly GameObject BombPrefab;
        private readonly BomberManPlayer BomberMan;
        private readonly EventManager Manager;
        private readonly BomberManPower BomberManPower;
        
        public AddBombButton(KeyCode addBombButton, GameObject bombPrefab) : base(addBombButton)
        {
            BombPrefab = bombPrefab;
            BomberMan = Object.FindObjectOfType<BomberManPlayer>();
            BomberManPower = BomberManPower.GetInstance();

            Manager = new EventManager(TypeActive.EnemyRebuildRoute);
            Manager.Subscribe(TypeActive.EnemyRebuildRoute, new RebuildRoute());
        }

        public override void Execute()
        {
            if (!Check())
            {
                return;
            }
            
            Object.Instantiate(BombPrefab, new Vector2(Mathf.Round(BomberMan.transform.position.x), 
                Mathf.Round(BomberMan.transform.position.y)), BomberMan.transform.rotation);
         
            Manager.Notify(TypeActive.EnemyRebuildRoute);
        }

        private bool Check()
        {
            var size = new Vector2(BomberMan.sensorSize, BomberMan.sensorSize);
            
            var bombs = GameObject.FindGameObjectsWithTag(Tag);
            return CheckKeyDown() && 
                   bombs.Length < BomberManPower.BombsAllowed && 
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