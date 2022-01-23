using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public interface IParallelTestService
    {
        Task Invoke();
    }
}