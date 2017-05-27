using System.Threading.Tasks;

namespace Excalibur.Tests.Cross.Core.Services.Interfaces
{
    public interface ILoginService
    {
        Task LoginAsync(string email, string password);
        Task<bool> ValidateAsync();
    }
}
