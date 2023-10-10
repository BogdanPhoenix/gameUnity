using Enum;

namespace ChainResponsibility.Direction
{
    public abstract class ChooseDirection
    {
        private ChooseDirection Next;
        private const DirectionPerson DefaultValue = DirectionPerson.Stop;

        public static ChooseDirection Link(ChooseDirection first, params ChooseDirection[] chain)
        {
            ChooseDirection head = first;

            foreach (var nextInChain in chain)
            {
                head.Next = nextInChain;
                head = nextInChain;
            }

            return first;
        }

        protected DirectionPerson CheckNext()
        {
            return Next?.Check() ?? DefaultValue;
        }

        public abstract DirectionPerson Check();
    }
}