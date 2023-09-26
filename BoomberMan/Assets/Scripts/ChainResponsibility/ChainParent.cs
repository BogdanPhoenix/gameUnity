namespace ChainResponsibility
{
    public abstract class ChainParent
    {
        protected ChainParent Next;
        
        public static T Link<T>(T first, params T[] chain) where T : ChainParent
        {
            T head = first;

            foreach (var nextInChain in chain)
            {
                head.Next = nextInChain;
                head = nextInChain;
            }

            return first;
        }
    }
}