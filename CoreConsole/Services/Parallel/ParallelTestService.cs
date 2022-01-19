using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services
{
    public class ParallelTestService : IParallelTestService
    {
        private readonly IConsole _console;
        private readonly HttpClient _httpClient;


        public ParallelTestService(IConsole console, IHttpClientFactory clientFactory)
        {
            _console = console;
            _httpClient = clientFactory.CreateClient();
        }

        public async Task Invoke()
        {
            var count = 0;
            while (true)
            {
                _httpClient.GetAsync($"https://localhost:44372/api/LongRunningRequest?ThreadId=thr{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()}&ExternalId=ext{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()}");
                count++;
                _console.WriteLine($"Request {count} sent");

                Console.ReadLine();
            }
        }
    }
}
