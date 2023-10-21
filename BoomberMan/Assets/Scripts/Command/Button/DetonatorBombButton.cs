using BomberMan;
using ChainResponsibility.Command.Button;
using UnityEngine;

namespace Command.Button
{
    public class DetonatorBombButton : ButtonActiveCommand
    {
        private readonly BomberManPower BomberManPower;

        public DetonatorBombButton(KeyCode detonateButton) : base(detonateButton)
        {
            BomberManPower = BomberManPower.GetInstance();
        }

        public override void Execute()
        {
            if (!CheckKeyDown() || !BomberManPower.HasDetonator)
            {
                return;
            }
            
            var bombs = Object.FindObjectsOfType<Bomb>();
            foreach (var bomb in bombs)
            {
                bomb.Blow();
            }
        }
    }
}