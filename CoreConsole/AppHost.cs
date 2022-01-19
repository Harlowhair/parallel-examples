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

        private readonly IParallelTestService _parallel1Service;

        public AppHost(IConsole console, IParallelTestService parallel1Service)
        {
            _console = console;
            _parallel1Service = parallel1Service;
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
            _console.WriteLine($"{(Verbose ? "" : "not ")}running in verbose mode");
            //_dataService.Invoke();
            //_testService.Invoke();
            //await _scrapingService.Test(Url, cancellationToken);
            //await _httpService.Invoke();

            await _parallel1Service.Invoke();

            return 0;
        }



    }
}
