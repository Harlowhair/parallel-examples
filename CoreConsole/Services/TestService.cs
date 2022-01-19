using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConsole.Services
{
    public class TestService : ITestService
    {
        private readonly IConsole _console;

        public TestService(IConsole console)
        {
            _console = console;
        }

        public void Invoke()
        {
            _console.WriteLine("Testing dependency injection!");
        }
    }
}
