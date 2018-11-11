using Excalibur.Base.Providers;
using Excalibur.Cross.ViewModels;
using Excalibur.Providers.EncryptedFileStorage;
using Excalibur.Tests.Encrypted.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Encrypted.Cross.Core.State;
using Excalibur.Tests.Encrypted.Cross.Core.Utils;
using MvvmCross;
using MvvmCross.Commands;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels
{
    public class PinLoginViewModel : BaseViewModel
    {
        private readonly IApplicationState _state;
        private readonly ILoginService _loginService;

        private string _pin = "";
        private bool _showError;

        public PinLoginViewModel(IApplicationState state, ILoginService loginService)
        {
            _state = state;
            _loginService = loginService;
        }
        public string Pin
        {
            get => _pin;
            set
            {
                SetProperty(ref _pin, value.TrimIllegalCharacters());

                if (_pin.Length < ExampleConstants.PinRequirements.Length)
                {
                    ShowError = false;
                }

                //Notify subs that CanLogin is changed
                RaisePropertyChanged(nameof(CanLogin));
            }
        }

        public bool ShowError
        {
            get => _showError;
            set => SetProperty(ref _showError, value);
        }

        public bool CanLogin => Pin?.Length == ExampleConstants.PinRequirements.Length && LoginCommand.CanExecute();

        public IMvxAsyncCommand LoginCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    var config = Mvx.IoCProvider.Resolve<IEncryptedProviderConfig>();
                    if (await config.InitializeAndTryDecrypt(Pin))
                    {
                        await _state.InitAndLoadAsync();
                        if (Mvx.IoCProvider.TryResolve<IEncryptedProviderConfiguration>(out var encryptedProviderConfiguration))
                        {
                            encryptedProviderConfiguration.ConfigureKey(config.DeviceKey());
                        }

                        if (await _loginService.ValidateAsync().ConfigureAwait(false))
                        {
                            await NavigationService.Navigate<MainViewModel>();
                        }
                        else
                        {
                            // todo unload?
                            ShowError = true;
                        }
                    }
                    else
                    {
                        // todo unload?
                        ShowError = true;
                    }
                });
            }
        }
    }
}
