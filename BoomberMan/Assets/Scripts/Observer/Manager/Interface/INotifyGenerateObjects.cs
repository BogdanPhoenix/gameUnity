using Enum;

namespace Observer.Manager.Interface
{
    public interface INotifyGenerateObjects
    {
        void Notify(TypeActive type, TypeObject[,] field);
    }
}