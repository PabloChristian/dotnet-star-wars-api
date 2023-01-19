using FluentValidation;
using StarWars.Application.Starships.Query;
using StarWars.Domain.ViewModels.Starships;

namespace StarWars.Application.Starships.Queries.GetStarshipList
{
    public class GetStarshipQuery : StarshipQuery<List<StarshipViewModel>>
    {
        public int Page { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetStarshipQueryValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class GetStarshipQueryValidator : AbstractValidator<GetStarshipQuery>
        {
            public GetStarshipQueryValidator()
            {
                RuleFor(x => x.Page)
                    .NotNull()
                    .GreaterThan(0);
            }
        }
    }
}
