using System;
using System.Threading.Tasks;
using Excalibur.Tests.FormsCross.Business.Interfaces;
using Excalibur.Tests.FormsCross.Domain;
using Excalibur.Tests.FormsCross.Services.Interfaces;
using Excalibur.Tests.FormsCross.State;
using Xciles.Uncommon.Net;
using XLabs.Ioc;

namespace Excalibur.Tests.FormsCross.Services
{
    public class LoginService : ILoginService
    {
        public async Task<bool> LoginAsync(string email, string password)
        {
            var state = Resolver.Resolve<IApplicationState>();
            state.Email = email;
            await state.SaveAsync();

            // simulate user logging in and retrieving
            // Usually we get some kind of profile returned, simulating this
            var rand = new Random();

            var url = $"https://jsonplaceholder.typicode.com/users/{rand.Next(1, 10)}";
            var result = await UncommonRequestHelper.ProcessGetRequestAsync<LoggedInUser>(url);

            await Task.Delay(1000);

            var loggedInUserBusiness = Resolver.Resolve<ILoggedInUser>();
            await loggedInUserBusiness.Store(result.Result);

            return true;
        }

        public async Task<bool> ValidateAsync()
        {
            var state = Resolver.Resolve<IApplicationState>();
            if (!string.IsNullOrWhiteSpace(state.Email))
            {
                // we load the current user from storage, usually we reauth and sync current user

                var loggedInUserBusiness = Resolver.Resolve<ILoggedInUser>();
                await loggedInUserBusiness.PublishFromStorageAsync().ConfigureAwait(false);

                return true;
            }
            return false;
        }
    }
}
