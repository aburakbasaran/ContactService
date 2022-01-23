using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.Infrastructure;
using ContactService.Infrastructure.Provider.Bus.RabbitQueue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactService.ReportCreator
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            ListenReportQueue(host.Services).GetAwaiter();

            return host.RunAsync();

        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddCoreInfrastructure()
               );
        }

        public static async Task ListenReportQueue(IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var rabbitBus = provider.GetRequiredService<IBus>();

            await rabbitBus.ReceiveAsync<Guid>(Queue.Processing, async ReportId =>
            {
                CreateReportCommand request = new()
                {
                    ReportId = ReportId
                };

                using var httpResponse = await new HttpClient().PostAsJsonAsync("https://localhost:44397/CreateUserCountReportData", ReportId.ToString());

            });

        }
    }
}
