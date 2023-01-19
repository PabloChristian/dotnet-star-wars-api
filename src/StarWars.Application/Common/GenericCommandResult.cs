using FluentValidation.Results;
using StarWars.Shared.Kernel.Commands;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;

namespace StarWars.Application.Common
{
    public abstract class GenericCommandResult<T> : ICommandResult<T>
    {
        protected GenericCommandResult() => ValidationResult = new ValidationResult();
        protected ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
        public virtual IList<ValidationFailure> GetErrors() => ValidationResult.Errors;
        private readonly IMediatorHandler _mediatorHandler;

        public GenericCommandResult(IMediatorHandler mediatorHandler) { _mediatorHandler = mediatorHandler; }

        public async Task SendErrors(CancellationToken cancellationToken)
        {
            foreach (var error in GetErrors())
                await _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage), cancellationToken);
        }
    }
}
