using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace Excalibur.Tests.Cross.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            // login things
            ShowViewModel<MainViewModel>();
        }
    }
}
