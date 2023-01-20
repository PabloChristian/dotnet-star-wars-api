using FluentValidation;
using StarWars.Domain.Properties;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Identity;

namespace StarWars.Application.Identity.Commands
{
    public class RegisterUserCommand : LoginCommand<TokenJwt>
    {
        public override bool IsValid()
        {
            ValidationResult = new RegisterUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegisterUserCommandValidator : LoginValidator<RegisterUserCommand>{}
    }
}
