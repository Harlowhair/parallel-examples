using McMaster.Extensions.CommandLineUtils;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreConsole.Scraping
{
    public class PuppeteerService : IPuppeteerService
    {
        private readonly IConsole _console;

        public PuppeteerService(IConsole console)
        {
            _console = console;
        }

        public async Task Test(string Url, CancellationToken cancellationToken)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var navWaitForIdle = new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.Networkidle0 } };
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                IgnoreHTTPSErrors = true,
                Timeout = 60 * 1000,
                Args = new[]
                {
                    //"--proxy-server=108.59.14.208:13040",
                    "--allow-file-access-from-files"
                }
            }))
            {
                using (var page = await browser.NewPageWithScriptsAsync())
                {
                    await page.GoToAsync(Url, navWaitForIdle);
                    while (!cancellationToken.IsCancellationRequested)
                    {

                    }
                }
            }
        }
    }
}
