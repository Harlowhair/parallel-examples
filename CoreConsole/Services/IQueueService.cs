using System.Threading.Tasks;

namespace CoreConsole.Services
{
    public interface IQueueService
    {
        Task<string> ReceiveMessage();
        Task SendMessage(string newMessage);
    }
}