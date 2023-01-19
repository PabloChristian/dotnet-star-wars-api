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

        internal class AuthenticateUserValidator : LoginValidator<AuthenticateUserCommand>
        {
            protected override void StartRules() => base.StartRules();
        }
    }
}
