using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Scraping
{
    public static class PuppeteerExtensions
    {
        static List<string> jsScripts;

        public static async Task<Page> NewPageWithScriptsAsync(this Browser browser)
        {
            if (jsScripts == null)
            {
                jsScripts = new List<string>();
                foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\ClientScripts", "*.js"))
                {
                    jsScripts.Add(new FileInfo(file).Name);
                }
            }

            var page = await browser.NewPageAsync();
            if (jsScripts.Count > 0)
            {
                page.DOMContentLoaded += async (sender, e) =>
                {
                    try
                    {
                        foreach (var jsFile in jsScripts)
                        {
                            await page.AddScriptTagAsync(new AddTagOptions
                            {
                                Path = $"ClientScripts/{jsFile}"
                            }); ;
                        }
                    }
                    catch { }
                };
            }

            return page;
        }

        public static async Task<Response> GoToWithRetryAsync(this Page page, string url, int maxRetries, NavigationOptions navigationOptions)
        {
            var loaded = false;
            var attempt = 1;
            Exception exception = null;
            while (!loaded && attempt <= maxRetries)
            {
                try
                {
                    var result = await page.GoToAsync(url, navigationOptions);
                    loaded = true;
                    return result;

                }
                catch (Exception ex)
                {
                    exception = ex;
                    attempt++;
                }
            }

            throw exception;
        }
    }
}
