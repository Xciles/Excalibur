using System.Threading.Tasks;
using Excalibur.Tests.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Cross.Core.State;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.Services
{
    public class LoginService : ILoginService
    {
        public async Task LoginAsync(string email, string password)
        {
            var state = Resolver.Resolve<IApplicationState>();
            state.Email = email;
            await state.SaveAsync();
        }

        public Task<bool> ValidateAsync()
        {
            var state = Resolver.Resolve<IApplicationState>();
            if (!string.IsNullOrWhiteSpace(state.Email))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
