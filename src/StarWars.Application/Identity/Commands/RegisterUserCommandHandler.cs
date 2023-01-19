using MediatR;
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
            TokenJwt token = new(true, string.Empty);

            if (request.IsValid())
            {
                try
                {
                    var user = _identityService.Authenticate(request.UserName, request.Password);

                    if (user != null)
                    {
                        token = _identityService.GetToken(user.Id, user.UserName);
                        await _unitOfWork.CommitAsync(cancellationToken);
                    }
                    else
                    {
                        await _mediatorHandler.RaiseEvent(new DomainNotification("Error", "User not found"));
                    }
                }
                catch (Exception e)
                {
                    await _mediatorHandler.RaiseEvent(new DomainNotification("Exception", e.Message));
                }
            }
            else
            {
                foreach (var error in request.GetErrors())
                    await _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage));
            }

            return await Task.FromResult(token);
        }
    }
}
