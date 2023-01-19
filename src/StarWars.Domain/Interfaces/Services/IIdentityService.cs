using StarWars.Domain.Entity;
using StarWars.Shared.Kernel.Identity;

namespace StarWars.Domain.Interfaces.Services
{
    public interface IIdentityService
    {
        User Authenticate(string username, string password);
        TokenJwt GetToken(Guid id, string username);
        Task<User> Register(string username, string password, CancellationToken cancellationToken);
    }
}
