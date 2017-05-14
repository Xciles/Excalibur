using System;
using Excalibur.Shared.Business;
using MvvmCross.Platform.Core;

namespace Excalibur.Cross.Business
{
    public class ExMainThreadDispatcher : IExMainThreadDispatcher
    {
        public bool InvokeOnMainThread(Action action)
        {
            return MvvmCross.Platform.Mvx.Resolve<IMvxMainThreadDispatcher>().RequestMainThreadAction(action);
        }
    }
}
