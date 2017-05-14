using System;

namespace Excalibur.Shared.Business
{
    public interface IExMainThreadDispatcher
    {
        bool InvokeOnMainThread(Action action);
    }
}
