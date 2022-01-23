using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public interface IParallelForEachService
    {
        Task Invoke();
    }
}