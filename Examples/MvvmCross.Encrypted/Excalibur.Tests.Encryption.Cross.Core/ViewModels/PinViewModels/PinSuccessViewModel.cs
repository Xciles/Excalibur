using Excalibur.Cross.ViewModels;
using MvvmCross.Commands;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels
{
    public class PinSuccessViewModel : BaseViewModel
    {
        public PinSuccessViewModel()
        {

        }

        public IMvxAsyncCommand ContinueCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await NavigationService.Navigate<MainViewModel>();
                });
            }
        }
    }
}
