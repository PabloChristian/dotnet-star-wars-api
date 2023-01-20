using FluentValidation.Results;
using StarWars.Shared.Kernel.Commands;

namespace StarWars.Application.Common
{
    public abstract class GenericCommandResult<T> : ICommandResult<T>
    {
        public abstract bool IsValid();
        public virtual IList<ValidationFailure> GetErrors() => ValidationResult.Errors;

        protected ValidationResult ValidationResult { get; set; }

        protected GenericCommandResult() => ValidationResult = new ValidationResult();
    }
}
