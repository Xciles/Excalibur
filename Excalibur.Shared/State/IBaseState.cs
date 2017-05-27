using System.Threading.Tasks;

namespace Excalibur.Shared.State
{
    public interface IBaseState<TConfig>
    {
        Task InitAndLoadAsync();
        Task SaveAsync();
    }
}