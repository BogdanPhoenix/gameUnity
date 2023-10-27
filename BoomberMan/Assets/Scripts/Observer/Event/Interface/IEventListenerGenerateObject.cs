using Enum;

namespace Observer.Event.Interface
{
    public interface IEventListenerGenerateObject
    {
        void CreateObjects(TypeObject[,] field);
    }
}