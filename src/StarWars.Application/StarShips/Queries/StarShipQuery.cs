using FluentValidation;
using StarWars.Application.Common;

namespace StarWars.Application.Starships.Query
{
    public class StarshipQuery<TResult> : GenericCommandResult<TResult>
    {
        public override bool IsValid()
        {
            ValidationResult = new StarshipQueryValidator<StarshipQuery<TResult>>().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class StarshipQueryValidator<T> : AbstractValidator<T> where T : StarshipQuery<TResult>
        {
        }
    }
}
