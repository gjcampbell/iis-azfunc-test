
using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using NLog;

[assembly: FunctionsStartup(typeof(IIS.FuncTest.Startup))]
namespace IIS.FuncTest
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var logFactory = LogManager.LoadConfiguration(GetLogConfigFilePath());
            builder.Services.AddLogging(b => b.AddNLog());
        }

        private string GetLogConfigFilePath()
        {
            var assmPath = typeof(Startup).Assembly.Location;
            var binDir = Path.GetDirectoryName(assmPath);
            var result = Path.Combine(binDir, "..\\nlog.config");
            return result;
        }
    }
}
