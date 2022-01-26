using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public class ParallelQueueService : BaseParallelService, IParallelQueueService
    {
        private readonly IQueueService _queueService;

        public ParallelQueueService(IConsole console, IHttpClientFactory clientFactory, IQueueService queueService) : base(console, clientFactory)
        {
            _queueService = queueService;
        }

        public async Task FillQueue(int items)
        {
            for (int i = 1; i <= items; i++)
            {
                _queueService.SendMessage(i.ToString());
            }

            _console.WriteLine($"{items}s queued");
        }

        public async Task ProcessQueue()
        {
            var msg = await _queueService.ReceiveMessage();
            while (!string.IsNullOrWhiteSpace(msg))
            {
                LongRunningRequest();
                _console.WriteLine($"Request {msg} sent");
                msg = await _queueService.ReceiveMessage();
            }
        }

    }
}
