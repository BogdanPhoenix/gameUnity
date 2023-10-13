using Observer;
using Observer.Event;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ChainResponsibility.Command.Button
{
    public class AddBombButton : ButtonActiveCommand
    {
        private const string Tag = "Bomb";
        
        private readonly GameObject BombPrefab;
        private readonly BomberMan BomberMan;
        private readonly EventManager Manager;
        
        public AddBombButton(KeyCode addBombButton, GameObject bombPrefab) : base(addBombButton)
        {
            BombPrefab = bombPrefab;
            BomberMan = Object.FindObjectOfType<BomberMan>();

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
            var size = new Vector2(BomberMan.SensorSize, BomberMan.SensorSize);
            
            var bombs = GameObject.FindGameObjectsWithTag(Tag);
            return CheckKeyDown() && 
                   bombs.Length < BomberMan.GetExtraBomb() && 
                   !CheckLayer(size, BomberMan.BombLayer) && 
                   !CheckLayer(size, BomberMan.FireLayer) && 
                   !CheckLayer(size, BomberMan.BrickLayer);
        }
        
        private bool CheckLayer(Vector2 size, LayerMask layer)
        {
            return Physics2D.OverlapBox(BomberMan.Sensor.position, size, 0, layer);
        }
    }
}