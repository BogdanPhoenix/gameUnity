using UnityEngine;

namespace Command.Button
{
    public abstract class ButtonActiveCommand
    {
        private readonly KeyCode ChooseButton;

        protected ButtonActiveCommand(KeyCode chooseButton)
        {
            ChooseButton = chooseButton;
        }

        protected bool CheckKeyDown()
        {
            return Input.GetKeyDown(ChooseButton);
        }

        public abstract void Execute();
    }
}