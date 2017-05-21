using System.Threading.Tasks;

namespace Excalibur.Shared.Configuration
{
    public interface IConfigurationManager
    {
        Task<TConfigObject> LoadAsync<TConfigObject>() where TConfigObject : new();
        Task<bool> SaveAsync<TConfigObject>(TConfigObject configObject) where TConfigObject : new();
    }
}
