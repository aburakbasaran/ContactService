using System.Threading;
using System.Threading.Tasks;

namespace ContactService.Application.Interface
{
    public interface IBusinessRule
    {
        Task<bool> IsBroken(CancellationToken cancellationToken);

        string Message { get; }
    }
}
