using System.Threading.Tasks;

namespace Excalibur.Tests.FormsCross.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string email, string password);
        Task<bool> ValidateAsync();
    }
}
