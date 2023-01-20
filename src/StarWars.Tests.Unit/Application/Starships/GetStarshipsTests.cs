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
using StarWars.Infrastructure.HttpAdapters.Starships;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StarWars.Tests.Unit.Application.Identity
{
    public class GetStarshipsTests
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IStarshipAdapter> _mockStarshipAdapter;

        public GetStarshipsTests()
        {
            _mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
            _mockMediator = new Mock<IMediator>();
            _mediatorHandler = new MediatorHandler(_mockMediator.Object);
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
            _mockStarshipAdapter = new Mock<IStarshipAdapter>();
            _mockStarshipAdapter.Setup(x => x.GetStarships(It.IsAny<int>()))
                .Returns(Task.FromResult(starshipResultData));
        }

        [Fact]
        public async Task Should_not_get_starships()
        {
            //Arrange
            var starshipQuery = new GetStarshipQuery();
            var mockStarshipAdapter = new Mock<IStarshipAdapter>();
            mockStarshipAdapter.Setup(x => x.GetStarships(100)).Returns(value: null!);
            var handler = new GetStarshipQueryHandler(mockStarshipAdapter.Object, _mapper, _mediatorHandler);

            //Act
            Func<Task> action = () => handler.Handle(starshipQuery, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<IntegrationException>();
        }

        [Fact]
        public async Task Should_get_starships()
        {
            //Arrange
            var starshipQuery = new GetStarshipQuery();
            var handler = new GetStarshipQueryHandler(_mockStarshipAdapter.Object, _mapper, _mediatorHandler);

            //Act
            var result = await handler.Handle(starshipQuery, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Test2")]
        [InlineData("Test3")]
        public async Task Should_get_starships_by_filter_manufacturer(string manufacturer)
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
                        Manufacturer = manufacturer
                    },
                    new StarshipDataResult()
                    {
                        Name = "Test2",
                        Manufacturer = manufacturer
                    },
                    new StarshipDataResult()
                    {
                        Name = "Test3",
                        Manufacturer = manufacturer
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
            result.Should().OnlyContain(
                x => x.Manufacturer.Equals(manufacturer, StringComparison.InvariantCultureIgnoreCase)
            );
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Should_not_get_starships_invalid_data(int page)
        {
            //Arrange
            var starshipQuery = new GetStarshipQuery(page);

            //Act
            var handler = new GetStarshipQueryHandler(_mockStarshipAdapter.Object, _mapper, _mediatorHandler);
            var result = await handler.Handle(starshipQuery, new CancellationToken());
            var isValid = starshipQuery.IsValid();
            var errors = starshipQuery.GetErrors();

            //Assert
            result.Should().BeNull();
            isValid.Should().BeFalse();
            errors.Should().NotBeEmpty();
            errors.Should().HaveCountGreaterThan(0);
        }
    }
}
