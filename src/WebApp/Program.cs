using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IRU.LargeFileUploader.WebApp
{
    public class Program
    {
        private const int K = 1024;

        private const int M = K * K;

        public static async Task Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel(options =>
                    options.Limits.MaxRequestBodySize = M)
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            await host.RunAsync(CancellationToken.None);
        }
    }
}
