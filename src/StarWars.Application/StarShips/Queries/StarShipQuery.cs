using FluentValidation;
using StarWars.Application.Common;

namespace StarWars.Application.Starships.Query
{
    public class StarshipQuery<TResult> : GenericCommandResult<TResult>
    {
        public int Page { get; set; } = 1;
        public string? Manufacturer { get; set; } = "";

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
