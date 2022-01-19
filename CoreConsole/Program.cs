using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CoreConsole.Config;
using CoreConsole.Data;
using CoreConsole.Scraping;
using CoreConsole.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreConsole
{
    class Program
    {
        private static IConfiguration _config;


        static async Task<int> Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

            _config = builder.Build();

            var services = ConfigureServices();

            var app = new CommandLineApplication<AppHost>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(services);

            //The cancellation token allows the app to exit gracefully - Ctrl+C will send a cancellation request which can be handled rather than ending immediately
            var cts = new CancellationTokenSource();
            ConsoleCancelEventHandler cancelHandler = (o, e) =>
            {
                Console.WriteLine("Ctrl+C detected");
                cts.Cancel();
                e.Cancel = true;
            };

            Console.CancelKeyPress += cancelHandler;

            return await app.ExecuteAsync(args, cts.Token);

        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IConsole>(PhysicalConsole.Singleton)
                .AddSingleton<IConfiguration>(_config)
                .AddSingleton<ITestService, TestService>()
                .AddSingleton<IHttpClientTestService, HttpClientTestService>()
                .AddSingleton<IDataService, DataService>()
                .AddSingleton<IQueueService, QueueService>()
                .AddSingleton<IPuppeteerService, PuppeteerService>()
                .AddSingleton<IParallelTestService, ParallelTestService>()
                .AddDbContext<DataContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly(typeof(DataContext).Assembly.GetName().Name)))
                .AddHttpClient();

            services.AddOptions();
            services.Configure<QueueConfig>(_config.GetSection("QueueSettings"));
            return services.BuildServiceProvider();
        }


        //Design time support to handle migrations
        private static IServiceProvider ConfigureDesignTimeServices()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

            _config = builder.Build();
            IServiceCollection isc = new ServiceCollection();
            isc.AddSingleton<IConfiguration>(_config)
                .AddDbContext<DataContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly(typeof(DataContext).Assembly.GetName().Name)));

            // create service provider
            return isc.BuildServiceProvider();
        }

        private class Factory : IDesignTimeDbContextFactory<DataContext>
        {
            public DataContext CreateDbContext(string[] args)
                => ConfigureDesignTimeServices().GetService<DataContext>();
        }
    }

}
