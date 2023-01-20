using FluentValidation;
using StarWars.Domain.Properties;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Identity;

namespace StarWars.Application.Identity.Commands
{
    public class AuthenticateUserCommand : LoginCommand<TokenJwt>
    {
        public override bool IsValid()
        {
            ValidationResult = new AuthenticateUserValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AuthenticateUserValidator : LoginValidator<AuthenticateUserCommand>{}
    }
}
