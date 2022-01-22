using System.Threading;
using System.Threading.Tasks;

namespace ContactService.Infrastructure.Common
{
    public interface IApiClient
    {
        Task<TResponse> Get<TResponse>(string url, double timeoutInSeconds, CancellationToken cancellationToken);
        Task<TResponse> Post<TRequest, TResponse>(string url, double timeoutInSeconds, TRequest request, CancellationToken cancellationToken);
        Task<TResponse> Put<TRequest, TResponse>(string url, double timeoutInSeconds, TRequest request, CancellationToken cancellationToken);
        Task<TResponse> Patch<TRequest, TResponse>(string url, double timeoutInSeconds, TRequest request, CancellationToken cancellationToken);
    }
}
