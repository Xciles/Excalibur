 using System;
using System.Net.Http;
using System.Threading.Tasks;
using Excalibur.Cross.Services;
using Excalibur.Tests.Encrypted.Cross.Core.Business.Interfaces;
using Excalibur.Tests.Encrypted.Cross.Core.Domain;
using Excalibur.Tests.Encrypted.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Encrypted.Cross.Core.State;
using MvvmCross;

namespace Excalibur.Tests.Encrypted.Cross.Core.Services
{
    public class LoginService : ServiceBase, ILoginService
    {
        private readonly IApplicationState _applicationState;

        public LoginService(IApplicationState applicationState)
        {
            _applicationState = applicationState;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            _applicationState.Email = email;

            // simulate user logging in and retrieving
            // Usually we get some kind of profile returned, simulating this
            var rand = new Random();

            var url = $"https://jsonplaceholder.typicode.com/users/{rand.Next(1, 10)}";

            using (var client = CreateDefaultHttpClient())
            {
                var responseMessage = await client.GetAsync(url);
                var result = responseMessage.ConvertFromJsonResponse<LoggedInUser>();

                await Task.Delay(1000);

                var loggedInUserBusiness = Mvx.IoCProvider.Resolve<ILoggedInUser>();
                loggedInUserBusiness.StoreForSaveAfterPin(result.Result);
            }

            return true;
        }

        public async Task<bool> ValidateAsync()
        {
            if (!string.IsNullOrWhiteSpace(_applicationState.Email))
            {
                // we load the current user from storage, usually we reauth and sync current user

                var loggedInUserBusiness = Mvx.IoCProvider.Resolve<ILoggedInUser>();
                loggedInUserBusiness.PublishFromStorage().ConfigureAwait(false);

                return true;
            }
            return false;
        }
    }
}
