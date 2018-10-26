using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.Utils;
using MvvmCross.Commands;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels
{
    public class RegisterPinViewModel : BaseViewModel

    {
        private string _pin;
        private bool _pinValid;

        public RegisterPinViewModel()
        {

        }

        public string Pin
        {
            get => _pin;
            set
            {
                SetProperty(ref _pin, value.TrimIllegalCharacters());

                //ToDo more extensive validation?
                PinValid = _pin.Length == ExampleConstants.PinRequirements.Length;
            }
        }

        public bool PinValid
        {
            get => _pinValid;
            set => SetProperty(ref _pinValid, value);
        }

        public IMvxAsyncCommand ContinueCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await NavigationService.Navigate<ConfirmPinViewModel, string>(Pin);
                });
            }
        }

        public override void ViewAppeared()
        {
            Pin = "";
            base.ViewAppeared();
        }
    }
}
