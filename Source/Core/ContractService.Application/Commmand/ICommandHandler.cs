using ContactService.Application.Model;
using MediatR;

namespace ContactService.Application.Commmand
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : ApiResponse
    {
    }
}
