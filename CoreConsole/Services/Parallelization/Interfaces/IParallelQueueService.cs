using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public interface IParallelQueueService
    {
        Task FillQueue(int items);
        Task ProcessQueue();
    }
}