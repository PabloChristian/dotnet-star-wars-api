using MediatR;
using StarWars.Shared.Kernel.Commands;

namespace StarWars.Shared.Kernel.Handler
{
    public interface IMediatorHandler
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
        Task<TResult> SendCommandResult<TResult>(ICommandResult<TResult> command, CancellationToken cancellationToken = default);
        Task RaiseEvent<T>(T @event, CancellationToken cancellationToken = default) where T : class;
        Task RaiseEvent<T>(T @event) where T : class;
    }
}
