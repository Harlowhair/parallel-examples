using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public class ParallelForEachService : BaseParallelService, IParallelForEachService
    {
        public ParallelForEachService(IConsole console, IHttpClientFactory clientFactory) : base(console, clientFactory)
        { }


        public async Task Invoke()
        {
            var list = Enumerable.Range(0, 100).ToList();

            Parallel.ForEach(list, item =>
            {
                LongRunningRequest();
                _console.WriteLine($"Request {item} sent");
            });


        }

        public async Task CPUBound()
        {
            var list = Enumerable.Range(0, 100).ToList();

            Parallel.ForEach(list, item =>
            {
                LongRunningRequest();
                _console.WriteLine($"Request {item} sent");
            });


        }

    }
}
