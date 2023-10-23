using BomberMan;
using Map.ActionsOnObjects;
using Map.Bomb;
using UnityEngine;

namespace Command.Button
{
    public class DetonatorBombButton : ButtonActiveCommand
    {
        private readonly BomberManPower BomberManPower;
        private readonly IActionActive<BombObject> Bombs;

        public DetonatorBombButton(KeyCode detonateButton) : base(detonateButton)
        {
            BomberManPower = BomberManPower.GetInstance();
            Bombs = BombOnMap.GetInstance();
        }

        public override void Execute()
        {
            if (!CheckKeyDown() || !BomberManPower.HasDetonator) return;

            Bombs.Active();
        }
    }
}