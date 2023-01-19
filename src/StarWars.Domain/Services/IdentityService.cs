using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StarWars.Domain.Interfaces.Services;
using StarWars.Domain.Interfaces.Repositories;
using StarWars.Domain.Entity;
using StarWars.Shared.Kernel.Identity;
using StarWars.Shared.Kernel.Helper;
using StarWars.Domain.Exceptions;

namespace StarWars.Domain.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public IdentityService(IUnitOfWork unitOfWork, IUserRepository userRepository, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _config = config;
        }

        public User Authenticate(string username, string password) =>
            _userRepository.GetByExpression(
                x => x.Username == username && x.Password == Cryptography.PasswordEncrypt(password)
            )?.FirstOrDefault();

        public TokenJwt GetToken(Guid id, string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return null;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, username)
                };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
            claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new TokenJwt(true, new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<User> Register(string username, string password, CancellationToken cancellationToken)
        {
            var userExists = _userRepository.GetByExpression(x => x.Username == username)?.FirstOrDefault();
            if (userExists != null) throw new DomainException(Properties.Resources.User_AlreadyExists);

            var user = new User(username, username, Cryptography.PasswordEncrypt(password));
            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Authenticate(username, password);
        }
    }
}
