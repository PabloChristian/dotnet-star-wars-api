using MediatR;

namespace StarWars.Shared.Kernel.Commands
{
    public interface ICommandResult<T> : IRequest<T>
    {
        bool IsValid();
    }
}
