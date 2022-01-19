using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services.Parallel
{
    public class ParallelForEachService : BaseParallelService
    {
        public ParallelForEachService(IConsole console, IHttpClientFactory clientFactory) : base(console, clientFactory)
        { }


    }
}
