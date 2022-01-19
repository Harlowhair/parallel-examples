using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services.Parallel
{
    public class BaseParallelService
    {
        protected readonly IConsole _console;
        protected readonly HttpClient _httpClient;

        public BaseParallelService(IConsole console, IHttpClientFactory clientFactory)
        {
            _console = console;
            _httpClient = clientFactory.CreateClient();
        }
    }
}
