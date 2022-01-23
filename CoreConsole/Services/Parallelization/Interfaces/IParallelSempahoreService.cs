using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public interface IParallelSempahoreService
    {
        Task Invoke();
    }
}