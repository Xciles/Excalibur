namespace Excalibur.Shared
{
    public class MessageBase<T>
    {
        public MessageBase(T @object)
        {
            Object = @object;
        }

        public T Object { get; set; }
    }
}
