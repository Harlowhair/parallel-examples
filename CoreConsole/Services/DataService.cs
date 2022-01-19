using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services
{
    public class DataService : IDataService
    {
        private readonly string _connectionString;
        private readonly IConsole _console;

        public DataService(IConfiguration configuration, IConsole console)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _console = console;
        }

        public void Invoke()
        {
            _console.WriteLine(_connectionString);
        }
    }
}
