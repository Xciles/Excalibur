using System.Threading.Tasks;

namespace Excalibur.Tests.Encrypted.Cross.Core.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string email, string password);
        Task<bool> ValidateAsync();
    }
}
