using System.Threading.Tasks;

namespace CoreConsole.Services
{
    public interface IParallelTestService
    {
        Task Invoke();
    }
}