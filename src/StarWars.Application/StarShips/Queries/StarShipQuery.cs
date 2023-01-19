using FluentValidation;
using StarWars.Application.Common;

namespace StarWars.Application.StarShips.Query
{
    public class StarShipQuery<TResult> : GenericCommandResult<TResult>
    {
        public override bool IsValid()
        {
            ValidationResult = new StarShipQueryValidator<StarShipQuery<TResult>>().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class StarShipQueryValidator<T> : AbstractValidator<T> where T : StarShipQuery<TResult>
        {
        }
    }
}
