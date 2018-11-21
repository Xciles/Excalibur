using System;
using System.Net.Http;
using System.Threading.Tasks;
using Excalibur.Cross.Services;
using Excalibur.Tests.Cross.Core.Business.Interfaces;
using Excalibur.Tests.Cross.Core.Domain;
using Excalibur.Tests.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Cross.Core.State;
using MvvmCross;

namespace Excalibur.Tests.Cross.Core.Services
{
    public class LoginService : ServiceBase, ILoginService
    {
        public async Task<bool> LoginAsync(string email, string password)
        {
            var state = Mvx.IoCProvider.Resolve<IApplicationState>();
            state.Email = email;
            await state.Save();

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
                await loggedInUserBusiness.Store(result.Result);
            }

            return true;
        }

        public async Task<bool> ValidateAsync()
        {
            var state = Mvx.IoCProvider.Resolve<IApplicationState>();
            if (!string.IsNullOrWhiteSpace(state.Email))
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
