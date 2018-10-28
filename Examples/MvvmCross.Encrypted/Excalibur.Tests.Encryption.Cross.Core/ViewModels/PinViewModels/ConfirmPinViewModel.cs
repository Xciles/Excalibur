using Excalibur.Cross.ViewModels;
using Excalibur.Providers.EncryptedFileStorage;
using Excalibur.Tests.Encrypted.Cross.Core.Business.Interfaces;
using Excalibur.Tests.Encrypted.Cross.Core.State;
using Excalibur.Tests.Encrypted.Cross.Core.Utils;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.IoC;

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
                    // init protected store as encrypted filestorage config
                    // we have to init ProtectedStore before in Android as well (based on pincode)

                    var config = Mvx.IoCProvider.Resolve<IEncryptedProviderConfig>();
                    await config.Init(ConfirmPin);

                    var tempEmail = _state.Email;
                    await _state.InitAndLoadAsync();

                    _state.Pin = ConfirmPin;
                    _state.Email = tempEmail;
                    await _state.SaveAsync();

                    var loggedInUserBusiness = Mvx.IoCProvider.Resolve<ILoggedInUser>();
                    await loggedInUserBusiness.Store();


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
