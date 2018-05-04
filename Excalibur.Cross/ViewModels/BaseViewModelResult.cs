using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace Excalibur.Cross.ViewModels
{
    /// <summary>
    /// BaseViewModel implementation that returns a <see cref=“TResult”/> when the viewmodel is closed.
    /// </summary>
    public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
    {
        public abstract TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }
    }
}