using Enum;
using UnityEngine;

namespace ChainResponsibility.Direction
{
    public class CheckDirection : ChooseDirection
    {
        private readonly DirectionPerson Direction;
        private readonly KeyCode KeyCode;

        public CheckDirection(KeyCode keyCode, DirectionPerson directionPerson)
        {
            KeyCode = keyCode;
            Direction = directionPerson;
        }

        public override DirectionPerson Check()
        {
            return Input.GetKey(KeyCode) ? Direction : CheckNext();
        }
    }
}