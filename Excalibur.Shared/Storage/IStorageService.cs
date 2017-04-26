using System.Threading.Tasks;

namespace Excalibur.Shared.Storage
{
    public interface IStorageService
    {
        Task<string> StoreAsync(string folder, string fullName, string contentAsString);
        Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes);
        Task<string> ReadAsTextAsync(string folder, string fullName);
        Task<byte[]> ReadAsBinaryAsync(string folder, string fullName);
        void DeleteFile(string folder, string fullName);
    }
}
