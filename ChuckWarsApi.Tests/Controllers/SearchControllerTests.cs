using AutoMapper;
using ChuckWarsApi.Controllers;
using ChuckWarsApi.Services.Chuck;
using ChuckWarsApi.Services.Swapi;
using ChuckWarsApi.Shared.Dtos.Chuck;
using ChuckWarsApi.Shared.Dtos.Swapi;
using ChuckWarsApi.Shared.Models.Search;
using ChuckWarsApi.Shared.Profiles;
using ChuckWarsWebAssembly.Shared.Profiles;
using NSubstitute;
using Shouldly;
using Xunit;

namespace ChuckWarsApi.Tests.Controllers
{
    public class SearchControllerTests
    {
        private static IMapper _mapper;

        public SearchControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ChuckProfile());
                mc.AddProfile(new SwapiProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async Task ItShouldSearch()
        {
            // Arrange
            var swapiService = Substitute.For<ISwapiService>();
            var chuckService = Substitute.For<IChuckService>();

            var expectedPerson = new PersonDto
            {
                Name = "Test",
                Mass = "heavy"
            };

            var expectedPeople = new PeopleDto
            {
                Count = 1,
                Next = "?page=2",
                Previous = null,
                Results = new List<PersonDto>
                {
                    expectedPerson
                }
            };

            var expectedJoke = new JokeDto
            {
                Icon_Url = "icon",
                Id = "2",
                Url = "url",
                Value = "value"
            };

            var expectedSearchResult = new SearchDto
            {
                Total = 1,
                Result = new List<JokeDto>
                {
                    expectedJoke
                }
            };

            chuckService.SearchJokeAsync(Arg.Any<string>()).Returns(expectedSearchResult);
            swapiService.SearchPeople(Arg.Any<string>()).Returns(expectedPeople);

            var controller = new SearchController(swapiService, chuckService, _mapper);

            // Act
            var result = await controller.Search("batman");

            // Assert
            result.ShouldBeAssignableTo<SearchResultModel>();
            
            var chuckResult = result.ChuckNorrisResults;
            var swapiResult = result.SwApiResults;

            chuckResult!.Total.ShouldBe(1);
            chuckResult.Result.Single(result =>
            {
                result.IconUrl.ShouldBe(expectedJoke.Icon_Url);
                result.Id.ShouldBe(expectedJoke.Id);
                result.Url.ShouldBe(expectedJoke.Url);
                result.Value.ShouldBe(expectedJoke.Value);

                return true;
            });

            swapiResult!.Count.ShouldBe(expectedPeople.Count);
            swapiResult.Next.ShouldBe(expectedPeople.Next);
            swapiResult.Previous.ShouldBe(expectedPeople.Previous);
            swapiResult.Results.Single(actual =>
            {
                actual.Name.ShouldBe(expectedPerson.Name);
                actual.Mass.ShouldBe(expectedPerson.Mass);

                return true;
            });
        }
    }
}
