using FluentValidation;
using MediatR;
using StarWars.Application.Common;
using StarWars.Shared.Kernel.Handler;
using System.Threading;

namespace StarWars.Application.Starships.Query
{
    public class StarshipQuery<TResult> : GenericCommandResult<TResult>
    {
        public int Page { get; set; } = 1;

        public override bool IsValid()
        {
            ValidationResult = new StarshipQueryValidator<StarshipQuery<TResult>>().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class StarshipQueryValidator<T> : AbstractValidator<T> where T : StarshipQuery<TResult>
        {
            public StarshipQueryValidator()
            {
                RuleFor(x => x.Page)
                    .NotNull()
                    .GreaterThan(0);
            }
        }
    }
}
