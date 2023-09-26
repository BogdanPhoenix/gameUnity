using Enum;
using UnityEngine;

namespace ChainResponsibility.Direction
{
    public class CheckDirection : ChooseDirection
    {
        private readonly KeyCode KeyCode;
        private readonly DirectionPerson Direction;

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