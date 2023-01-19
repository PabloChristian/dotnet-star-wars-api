using FluentValidation;
using StarWars.Application.Starships.Query;
using StarWars.Domain.ViewModels.Starships;

namespace StarWars.Application.Starships.Queries.GetStarshipByManufacture
{
    public class GetStarshipByManufacturerQuery : StarshipQuery<List<StarshipViewModel>>
    {
        public int Page { get; set; }
        public string Manufacturer { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetStarshipByManufacturerQueryValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class GetStarshipByManufacturerQueryValidator : AbstractValidator<GetStarshipByManufacturerQuery>
        {
            public GetStarshipByManufacturerQueryValidator()
            {
                RuleFor(x => x.Page)
                    .NotNull()
                    .GreaterThan(0);

                RuleFor(x => x.Manufacturer)
                    .NotEmpty();
            }
        }
    }
}
