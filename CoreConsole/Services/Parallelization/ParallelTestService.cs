using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public class ParallelTestService : BaseParallelService, IParallelTestService
    {

        public ParallelTestService(IConsole console, IHttpClientFactory clientFactory) : base(console, clientFactory)
        {

        }

        public async Task Invoke()
        {
            var count = 0;
            while(true)
            {
                Task.Run(() =>
                {
                    LongRunningRequest();
                });


                count++;
                _console.WriteLine($"Request {count} sent");
                Console.ReadLine();
            }

        }
    }
}
