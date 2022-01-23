using ContactService.Application.Model;
using MediatR;

namespace ContactService.Application.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : ApiResponse
    {
    }
}
