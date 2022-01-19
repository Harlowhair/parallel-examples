using System.Threading;
using System.Threading.Tasks;

namespace CoreConsole.Scraping
{
    public interface IPuppeteerService
    {
        Task Test(string Url, CancellationToken cancellationToken);
    }
}