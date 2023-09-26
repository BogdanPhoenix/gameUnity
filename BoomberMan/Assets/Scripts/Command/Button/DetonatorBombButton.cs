using UnityEngine;

namespace ChainResponsibility.Command.Button
{
    public class DetonatorBombButton : ButtonActiveCommand
    {
        public DetonatorBombButton(KeyCode detonateButton) : base(detonateButton) {}

        public override void Execute()
        {
            if (!CheckKeyDown())
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