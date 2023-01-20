using MediatR;
using StarWars.Domain.Entity;
using StarWars.Domain.Interfaces.Repositories;
using StarWars.Domain.Interfaces.Services;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Identity;
using StarWars.Shared.Kernel.Notifications;

namespace StarWars.Application.Identity.Commands
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, TokenJwt>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;
        private readonly IMediatorHandler _mediatorHandler;

        public AuthenticateUserCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediatorHandler, IIdentityService loginService)
        {
            _unitOfWork = unitOfWork;
            _mediatorHandler = mediatorHandler;
            _identityService = loginService;
        }

        public async Task<TokenJwt> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                foreach (var error in request.GetErrors())
                    await _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage), cancellationToken);

                return new TokenJwt(false, string.Empty);
            }

            TokenJwt token = new(true, string.Empty);
            var user = _identityService.Authenticate(request.Username, request.Password);
            token = await ValidateUser(token, user, cancellationToken);

            return await Task.FromResult(token);
        }

        private async Task<TokenJwt> ValidateUser(TokenJwt token, User user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                token = _identityService.GetToken(user.Id, user.Username);
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            else
            {
                await _mediatorHandler.RaiseEvent(new DomainNotification("Error", "User not found"));
            }

            return token;
        }
    }
}
