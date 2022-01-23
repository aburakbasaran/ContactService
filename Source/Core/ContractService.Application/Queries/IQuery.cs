using MediatR;

namespace ContactService.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
