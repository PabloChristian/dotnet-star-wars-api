using Moq;
using StarWars.Shared.Kernel.Handler;
using Xunit;
using FluentAssertions;
using StarWars.Infrastructure.ServiceBus;
using MediatR;
using StarWars.Application.Starships.Queries.GetStarshipList;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;
using AutoMapper;
using StarWars.Infrastructure.HttpAdapters.Starships.Results;
using StarWars.Application.AutoMapper;
using StarWars.Domain.Exceptions;

namespace StarWars.Tests.Unit.Application.Identity
{
    public class GetStarshipsTests
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly Mock<IMediator> _mockMediator;

        public GetStarshipsTests()
        {
            _mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
            _mockMediator = new Mock<IMediator>();
            _mediatorHandler = new MediatorHandler(_mockMediator.Object);
        }

        [Fact]
        public void Should_not_get_starships()
        {
            //Arrange
            var starshipQuery = new GetStarshipQuery();
            var mockStarshipAdapter = new Mock<IStarshipAdapter>();
            mockStarshipAdapter.Setup(x => x.GetStarships(100))
                .Returns(value: null!);
            var handler = new GetStarshipQueryHandler(mockStarshipAdapter.Object, _mapper, _mediatorHandler);

            //Act
            Action result = () => handler.Handle(starshipQuery, CancellationToken.None);

            //Assert
            result.Should().Throw<IntegrationException>();
            result.Should().BeNull();
        }

        [Fact]
        public async Task Should_get_starships()
        {
            //Arrange
            var starshipQuery = new GetStarshipQuery();
            var starshipResultData = new StarshipResult()
            {
                Results = new List<StarshipDataResult>()
                {
                    new StarshipDataResult()
                    {
                        Name = "Test",
                        Manufacturer = "Test"
                    },
                    new StarshipDataResult()
                    {
                        Name = "Test2",
                        Manufacturer = "Test2"
                    },
                    new StarshipDataResult()
                    {
                        Name = "Test3",
                        Manufacturer = "Test3"
                    }
                }
            };
            var mockStarshipAdapter = new Mock<IStarshipAdapter>();
            mockStarshipAdapter.Setup(x => x.GetStarships(It.IsAny<int>()))
                .Returns(Task.FromResult(starshipResultData));
            var handler = new GetStarshipQueryHandler(mockStarshipAdapter.Object, _mapper, _mediatorHandler);

            //Act
            var result = await handler.Handle(starshipQuery, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }
    }
}
