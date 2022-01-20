using MediatR;

namespace ContactService.Application.Commmand
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
