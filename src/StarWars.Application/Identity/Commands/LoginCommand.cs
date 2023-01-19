using FluentValidation;
using StarWars.Application.Common;

namespace StarWars.Application.Identity.Commands
{
    public class LoginCommand<TResult> : GenericCommandResult<TResult>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public override bool IsValid() => throw new NotImplementedException();

        internal class LoginValidator<T> : AbstractValidator<T> where T : LoginCommand<TResult>
        {
            public LoginValidator() => StartRules();

            protected virtual void StartRules()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
