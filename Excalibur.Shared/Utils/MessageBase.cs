using Excalibur.Shared.Business;

namespace Excalibur.Utils
{
    public class MessageBase<T>
    {
        public MessageBase(T @object)
        {
            Object = @object;
        }

        public MessageBase(T @object, EDomainState state)
        {
            Object = @object;
            State = state;
        }

        public T Object { get; set; }
        public EDomainState State { get; set; } = EDomainState.Updated;
    }
}
