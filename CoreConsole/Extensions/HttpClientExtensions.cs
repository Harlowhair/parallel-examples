using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreConsole.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<string> GetStringWithRetryAsync(this HttpClient client, string url, int retryWait = 5, int retryCount = 10)
        {
            int retries = 1;
            string result = "";
            while (retries <= retryCount)
            {
                try
                {
                    result = await client.GetStringAsync(url);
                    break;
                }
                catch (Exception ex)
                {
                    if (retries <= retryCount)
                    {
                        retries++;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return result;
        }

        public static async Task<JsonDocument> GetJsonAsync(this HttpClient client, string url)
        {
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            var result = await client.GetStringAsync(url);
            return JsonDocument.Parse(result);
        }
    }
}
