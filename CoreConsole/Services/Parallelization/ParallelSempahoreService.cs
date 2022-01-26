using CoreConsole.Services.Parallelization;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public class ParallelSempahoreService : BaseParallelService, IParallelSempahoreService
    {
        private const int MAX_CONCURRENT_REQUESTS = 5;

        private SemaphoreSlim semaphore;

        /// <summary>
        /// Semaphore 
        /// </summary>
        /// <param name="console"></param>
        /// <param name="clientFactory"></param>
        public ParallelSempahoreService(IConsole console, IHttpClientFactory clientFactory) : base(console, clientFactory)
        {
            semaphore = new SemaphoreSlim(MAX_CONCURRENT_REQUESTS);
        }


        public async Task Invoke()
        {
            var list = Enumerable.Range(0, 30).ToList();
            var tasks = new List<Task>();
            //No error handling for brevity - in reality semaphore should be released in a finally clause
            foreach (var i in list)
            {
                await semaphore.WaitAsync();
                tasks.Add(Task.Run(() =>
                {
                    LongRunningRequest();
                    semaphore.Release();
                }));
                _console.WriteLine($"Request {i} sent");
            }

            await Task.WhenAll(tasks);

        }
    }
}
