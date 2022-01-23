using ContactService.ContactModule.Messages.User.Command;
using ContactService.Infrastructure;
using ContactService.Infrastructure.Provider.Bus.RabbitQueue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ImageClassifier
{
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
                CreateReportCommand request = new();
                request.ReportId = ReportId;

                using HttpResponseMessage httpResponse = await new HttpClient().PostAsJsonAsync("https://localhost:44397/CreateUserCountReportData", ReportId.ToString());

            });

        }

        private async Task<TResponse> Postyuy<TRequest, TResponse>(string url, double timeoutInSeconds, TRequest request, CancellationToken cancellationToken)
        {
            using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            using HttpResponseMessage httpResponse = await new HttpClient() // TODO böyle olmaması lazım.
                .PostAsJsonAsync(url, request, cts.Token);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content
                .ReadFromJsonAsync<TResponse>(cancellationToken: cts.Token);
        }
    }
}
