using AutoMapper;
using ChuckWarsApi.Controllers;
using ChuckWarsApi.Services.Swapi;
using ChuckWarsApi.Shared.Dtos.Swapi;
using ChuckWarsApi.Shared.Profiles;
using NSubstitute;
using Shouldly;
using Xunit;

namespace ChuckWarsApi.Tests.Controllers
{
    public class SwapiControllerTests
    {
        private static IMapper _mapper;

        public SwapiControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SwapiProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async Task ItShouldGetPeople()
        {
            // Arrange 
            var swapiService = Substitute.For<ISwapiService>();

            var expectedPerson = new PersonDto
            {
                Name = "Test",
                Mass = "heavy"
            };

            var expectedPeople = new PeopleDto
            {
                Count = 1,
                Next = "?page=1",
                Previous = null,
                Results = new List<PersonDto>
                {
                    expectedPerson
                }
            };

            swapiService.GetPeopleAsync(1).Returns(expectedPeople);
            var controller = new SwapiController(swapiService, _mapper);

            // Act
            var result = await controller.GetPeople();

            // Assert
            result.Count.ShouldBe(expectedPeople.Count);
            result.Next.ShouldBe(expectedPeople.Next);
            result.Previous.ShouldBe(expectedPeople.Previous);

            result.Results.Single(actual =>
            {
                actual.Name.ShouldBe(expectedPerson.Name);
                actual.Mass.ShouldBe(expectedPerson.Mass);

                return true;
            });
        }
    }
}
