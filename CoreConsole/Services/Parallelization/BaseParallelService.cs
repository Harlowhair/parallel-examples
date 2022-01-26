using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services.Parallelization
{
    public class BaseParallelService
    {
        protected readonly IConsole _console;
        protected readonly HttpClient _httpClient;

        protected string LongRunningApiUrl
        {
            get
            {
                return "https://localhost:44372/api/LongRunningRequest?ThreadId=thr{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()}&ExternalId=ext{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()}";
            }
        }

        public BaseParallelService(IConsole console, IHttpClientFactory clientFactory)
        {
            _console = console;
            _httpClient = clientFactory.CreateClient();
        }

        public string LongRunningRequest()
        {
            var response = _httpClient.GetAsync($"https://localhost:44372/api/LongRunningRequest?ThreadId=thr{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()}&ExternalId=ext{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()}").Result;
            return response.StatusCode.ToString() ;
        }

        public string LongRunningRequestCPUBound()
        {
            string response = "";
            while (response == "")
            {
                response = _httpClient.GetAsync($"https://localhost:44372/api/LongRunningRequest?ThreadId=thr{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()}&ExternalId=ext{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()}").Result.StatusCode.ToString();
            }
 
            return response;
        }


    }
}
