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

        internal class RegisterUserCommandValidator : LoginValidator<RegisterUserCommand>
        {
            protected override void StartRules() => base.StartRules();
        }
    }
}
