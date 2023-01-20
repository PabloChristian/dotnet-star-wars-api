using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StarWars.Api.Controllers;
using StarWars.Application.Common;
using StarWars.Application.Identity.Commands;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Identity;
using StarWars.Shared.Kernel.Notifications;
using StarWars.Shared.Kernel.Results;
using StarWars.Tests.Unit.Fixture;
using System.Net;
using Xunit;

namespace StarWars.Tests.Unit.Api
{
    public class IdentityControllerTest : StarWarsContextFixture
    {
        private readonly Mock<IMediatorHandler> _mockMediator;
        private readonly DomainNotificationHandler _domainNotificationHandler;
        private readonly Mock<ILogger<IdentityController>> _mockLogger;

        public IdentityControllerTest()
        {
            _mockMediator = new Mock<IMediatorHandler>();
            _domainNotificationHandler = new DomainNotificationHandler();
            _mockMediator.Setup(x => x.RaiseEvent(It.IsAny<DomainNotification>()))
                .Callback<DomainNotification>((x) => _domainNotificationHandler.Handle(x, CancellationToken.None));
            _mockLogger = new Mock<ILogger<IdentityController>>();
        }

        [Fact]
        public async Task Should_not_get_authenticated_return_unathourized()
        {
            //Arrange
            var obj = new AuthenticateUserCommand { Username = "test", Password = "123" };
            _mockMediator.Setup(x => x.SendCommandResult(It.IsAny<GenericCommandResult<bool>>(), new CancellationToken())).Returns(Task.FromResult(false));

            //Act
            var result = await new IdentityController(_domainNotificationHandler, _mockMediator.Object, _mockLogger.Object).LoginAsync(obj) as UnauthorizedResult;

            //Assert
            result?.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Should_get_authenticated_token()
        {
            //Arrange
            const string tokenExpected = "asASDNdBNASbdaskjdbabksdavbsklDAPsdh";
            var obj = new AuthenticateUserCommand { Username = "pablo", Password = "123456" };
            _mockMediator.Setup(x => x.SendCommandResult(It.IsAny<GenericCommandResult<TokenJwt>>(), new CancellationToken())).Returns(Task.FromResult(new TokenJwt
            (
                true,
                "asASDNdBNASbdaskjdbabksdavbsklDAPsdh"
            )));

            //Act
            var result = (await new IdentityController(_domainNotificationHandler, _mockMediator.Object, _mockLogger.Object).LoginAsync(obj) as OkObjectResult)?.Value as ApiOkReturn;
            var token = result?.Data as TokenJwt;

            //Assert
            result?.Success.Should().BeTrue();
            tokenExpected.Should().Be(token?.Token);
        }
    }
}
