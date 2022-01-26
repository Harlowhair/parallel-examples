using CoreConsole.Scraping;
using CoreConsole.Services;
using CoreConsole.Services.Parallelization;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreConsole
{
    public class AppHost
    {
        private readonly IConsole _console;

        private readonly IParallelTestService _parallelTestService;
        private readonly IParallelForEachService _parallelForEachService;
        private readonly IParallelSempahoreService _parallelSempahoreService;
        private readonly IParallelQueueService _parallelQueueService;

        public AppHost(IConsole console, IParallelTestService parallelTestService, IParallelForEachService parallelForEachService, IParallelSempahoreService parallelSempahoreService, IParallelQueueService parallelQueueService)
        {
            _console = console;
            _parallelTestService = parallelTestService;
            _parallelForEachService = parallelForEachService;
            _parallelSempahoreService = parallelSempahoreService;
            _parallelQueueService = parallelQueueService;
        }

        #region Command Line Options

        [Option]
        public bool Verbose { get; set; }
        // Inferred type = NoValue
        // Inferred names = "-v", "--verbose"

        [Option]
        public string Url { get; set; }

        #endregion Command Line Options

        //Entry point
        private async Task<int> OnExecute(CancellationToken cancellationToken)
        {
            await _parallelTestService.Invoke();
            //await _parallelForEachService.Invoke();
            //await _parallelSempahoreService.Invoke();
            //await _parallelQueueService.FillQueue(100);
            //await _parallelQueueService.ProcessQueue();
            return 0;
        }



    }
}
