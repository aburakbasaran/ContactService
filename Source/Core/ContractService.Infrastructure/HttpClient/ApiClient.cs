using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Infrastructure.Common;
using ContactService.Infrastructure.Constant;

namespace ContactService.Infrastructure.HttpClient
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResponse> Get<TResponse>(string url, double timeoutInSeconds, CancellationToken cancellationToken)
        {
            using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            return await CreateHttpClient()
                .GetFromJsonAsync<TResponse>(url, cts.Token);
        }

        public async Task<TResponse> Post<TRequest, TResponse>(string url, double timeoutInSeconds, TRequest request, CancellationToken cancellationToken)
        {
            using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            using HttpResponseMessage httpResponse = await CreateHttpClient()
                .PostAsJsonAsync(url, request, cts.Token);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content
                .ReadFromJsonAsync<TResponse>(cancellationToken: cts.Token);
        }

        public async Task<TResponse> Put<TRequest, TResponse>(string url, double timeoutInSeconds, TRequest request, CancellationToken cancellationToken)
        {
            using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            using HttpResponseMessage httpResponse = await CreateHttpClient()
                .PutAsJsonAsync(url, request, cts.Token);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content
                .ReadFromJsonAsync<TResponse>(cancellationToken: cts.Token);
        }

        public async Task<TResponse> Patch<TRequest, TResponse>(string url, double timeoutInSeconds, TRequest request, CancellationToken cancellationToken)
        {
            using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            using HttpResponseMessage httpResponse = await CreateHttpClient()
                .PatchAsync(url, JsonContent.Create(request), cts.Token);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content
                .ReadFromJsonAsync<TResponse>(cancellationToken: cts.Token);
        }

        private System.Net.Http.HttpClient CreateHttpClient()
        {
            System.Net.Http.HttpClient httpClient = _httpClientFactory.CreateClient(ApiConstant.HttpClientName);


            return httpClient;
        }
    }
}
