using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services
{
    public class HttpClientTestService : IHttpClientTestService
    {
        private readonly IConsole _console;
        private readonly HttpClient _httpClient;

        public HttpClientTestService(IConsole console, IHttpClientFactory clientFactory)
        {
            _console = console;
            _httpClient = clientFactory.CreateClient();
        }

        public async Task Invoke()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
            string jsonStr = await response.Content.ReadAsStringAsync();
            _console.WriteLine(JValue.Parse(jsonStr).ToString(Newtonsoft.Json.Formatting.Indented));
        }
    }
}
