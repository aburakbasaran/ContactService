using ContactService.Infrastructure.Common;
using ContactService.Infrastructure.Constant;
using ContactService.Infrastructure.HttpClient;
using ContactService.Infrastructure.Provider.Bus.RabbitQueue;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace ContactService.Infrastructure
{
    public static class BootStrapper
    {
        public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient(ApiConstant.HttpClientName, httpClient =>
            {
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
            });
            services.AddSingleton<IApiClient, ApiClient>();
            services.AddSingleton(sp => RabbitCreator.CreateBus("localhost"));

            return services;
        }
    }
}
