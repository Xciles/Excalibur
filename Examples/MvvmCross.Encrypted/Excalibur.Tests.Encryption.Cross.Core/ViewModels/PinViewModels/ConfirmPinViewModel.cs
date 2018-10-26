using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.State;
using Excalibur.Tests.Encrypted.Cross.Core.Utils;
using MvvmCross.Commands;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels
{
    public class ConfirmPinViewModel : BaseViewModel<string>
    {
        private readonly IApplicationState _state;
        private string _pin;
        private string _confirmPin;
        private bool _showError;


        public ConfirmPinViewModel(IApplicationState state)
        {
            _state = state;
        }

        public string ConfirmPin
        {
            get => _confirmPin;
            set
            {
                SetProperty(ref _confirmPin, value.TrimIllegalCharacters());

                ShowError = ConfirmPin?.Length == ExampleConstants.PinRequirements.Length && ConfirmPin != _pin;

                RaisePropertyChanged(nameof(PinValid));
            }
        }

        public bool PinValid => _pin == _confirmPin;

        public bool ShowError
        {
            get => _showError;
            set => SetProperty(ref _showError, value);
        }

        public override void Prepare(string parameter)
        {
            _pin = parameter;
        }

        public IMvxAsyncCommand ContinueCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    _state.Pin = ConfirmPin;
                    await _state.SaveAsync();
                    await NavigationService.Navigate<PinSuccessViewModel>();
                });
            }
        }

        public IMvxAsyncCommand ReturnCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await NavigationService.Close(this);
                });
            }
        }
    }
}
