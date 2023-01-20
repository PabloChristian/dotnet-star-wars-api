using FluentValidation;
using StarWars.Application.Common;
using StarWars.Domain.Properties;
using StarWars.Shared.Kernel.Handler;

namespace StarWars.Application.Identity.Commands
{
    public class LoginCommand<TResult> : GenericCommandResult<TResult>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public override bool IsValid() => throw new NotImplementedException();

        public class LoginValidator<T> : AbstractValidator<T> where T : LoginCommand<TResult>
        {
            public LoginValidator() => ExecuteBaseRules();

            public void ExecuteBaseRules()
            {
                RuleFor(x => x.Username)
                    .NotEmpty()
                    .WithMessage(Resources.User_UserName_Required);

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage(Resources.User_Password_Required);
            }
        }
    }
}
