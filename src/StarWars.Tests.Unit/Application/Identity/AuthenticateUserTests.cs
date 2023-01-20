using Moq;
using StarWars.Application.Identity.Commands;
using StarWars.Domain.Entity;
using StarWars.Domain.Interfaces.Repositories;
using StarWars.Domain.Interfaces.Services;
using StarWars.Domain.Services;
using StarWars.Infrastructure.Data.Repositories;
using StarWars.Infrastructure.Data;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Helper;
using StarWars.Shared.Kernel.Notifications;
using Xunit;
using StarWars.Tests.Unit.Fixture;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using StarWars.Infrastructure.ServiceBus;
using MediatR;

namespace StarWars.Tests.Unit.Application.Identity
{
    public class AuthenticateUserTests : StarWarsContextFixture
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<IMediatorHandler> _mockMediatorHandler;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        private readonly DomainNotificationHandler _domainNotificationHandler;
        private readonly AuthenticateUserCommandHandler handler;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly Mock<IMediator> _mockMediator;

        public AuthenticateUserTests()
        {
            db = GetDbInstance();
            _unitOfWork = new UnitOfWork(db);
            _userRepository = new UserRepository(db);
            _mockMediatorHandler = new Mock<IMediatorHandler>();
            _mockMediator = new Mock<IMediator>();
            _mediatorHandler = new MediatorHandler(_mockMediator.Object);
            _domainNotificationHandler = new DomainNotificationHandler();
            _mockMediatorHandler.Setup(x => x.RaiseEvent(It.IsAny<DomainNotification>()))
                .Callback<DomainNotification>((x) => _domainNotificationHandler.Handle(x, CancellationToken.None));

            _userRepository.Add(new User
            (
                "test",
                "test",
                Cryptography.PasswordEncrypt("123456")
            ));
            _unitOfWork.Commit();

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Issuer"))]).Returns("Test");
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Duration"))]).Returns("120");
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Key"))]).Returns("IZpipYfLNJro403p");

            _identityService = new IdentityService(_unitOfWork, _userRepository, mockConfig.Object);
            handler = new AuthenticateUserCommandHandler(_unitOfWork, _mediatorHandler, _identityService);
        }

        [Theory]
        [InlineData("", "123356")]
        [InlineData("test", "")]
        [InlineData("", "")]
        public async Task Should_not_get_authenticated_invalid_data(string username, string password)
        {
            //Arrange
            var userAuth = new AuthenticateUserCommand { Username = username, Password = password };

            //Act
            var result = await handler.Handle(userAuth, CancellationToken.None);

            //Assert
            result.Token.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Should_get_authenticated()
        {
            //Arrange
            var userAuth = new AuthenticateUserCommand { Username = "test", Password = "123456" };

            //Act
            var result = await handler.Handle(userAuth, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();
            _mockMediatorHandler.Verify(x => x.RaiseEvent(It.IsAny<DomainNotification>), Times.Never);
            _domainNotificationHandler.HasNotifications().Should().BeFalse();
        }
    }
}
