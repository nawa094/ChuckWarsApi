using AutoMapper;
using ChuckWarsApi.Controllers;
using ChuckWarsApi.Services.Chuck;
using ChuckWarsApi.Shared.Dtos.Chuck;
using ChuckWarsApi.Shared.Models.Chuck;
using ChuckWarsWebAssembly.Shared.Profiles;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using Xunit;

namespace ChuckWarsApi.Tests.Controllers
{
    public class ChuckControllerTests
    {
        private static IMapper _mapper;

        public ChuckControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ChuckProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async Task ItShouldGetCategories()
        {
            // Arrange
            var chuckService = Substitute.For<IChuckService>();

            var expectedCategories = new List<string>
            {
                "Batman",
                "Spiderman",
                "Superman"
            };

            var categories = new CategoriesDto
            {
                Categories = expectedCategories
            };

            chuckService.GetCategoriesAsync().Returns(categories);

            var controller = new ChuckController(chuckService, _mapper);

            // Act
            var result = await controller.GetCategories();

            // Assert
            result.ShouldBeAssignableTo<ChuckCategoriesModel>();
            result.Categories.ShouldBe(expectedCategories);
        }

        [Fact]
        public async Task ItShouldGetRandomJoke()
        {
            // Arrange
            var chuckService = Substitute.For<IChuckService>();

            var expectedJokeDto = new JokeDto
            {
                Icon_Url = "icon",
                Id = "2",
                Url = "url",
                Value = "value"
            };

            chuckService.GetJokeAsync(Arg.Any<string>()).Returns(expectedJokeDto);
            var controller = new ChuckController(chuckService, _mapper);

            // Act
            var actionResult = await controller.GetRandomJoke("batman");
            var result = actionResult.Value;

            // Assert
            result.ShouldBeAssignableTo<ChuckJokeModel>();
            result!.Id.ShouldBe(expectedJokeDto.Id);
            result!.IconUrl.ShouldBe(expectedJokeDto.Icon_Url);
            result!.Url.ShouldBe(expectedJokeDto.Url);
            result!.Value.ShouldBe(expectedJokeDto.Value);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("wertyuiuytrewsxcvjgusdciauksdhc akusdhbciwudhbckhducwodhcbwhubweucbkawduhcWKEUHBFVWuefvbwugevkwuagfwajrfvbwajcapworjvqpnjrqpc")]
        public async Task ItShouldNotGetRandomJoke(string category)
        {
            // Arrange
            var chuckService = Substitute.For<IChuckService>();

            var expectedJokeDto = new JokeDto
            {
                Icon_Url = "icon",
                Id = "2",
                Url = "url",
                Value = "value"
            };

            chuckService.GetJokeAsync(Arg.Any<string>()).Returns(expectedJokeDto);
            var controller = new ChuckController(chuckService, _mapper);

            // Act
            var actionResult = await controller.GetRandomJoke(category);

            // Assert
            var result = actionResult.Result.ShouldBeAssignableTo<BadRequestObjectResult>();
            var errorMessage = result!.Value!.ToString();

            errorMessage!.ShouldContain("should be between 3 and 120");
        }

        [Fact]
        public async Task ItShouldSearchJoke()
        {
            // Arrange
            var chuckService = Substitute.For<IChuckService>();
            
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
            var controller = new ChuckController(chuckService, _mapper);

            // Act
            var response = await controller.SearchJoke("batman");
            var result = response.Value;

            // Assert
            response!.ShouldBeAssignableTo<ActionResult<ChuckSearchModel>>();

            var searchResult = result.Result.Single();

            searchResult!.Id.ShouldBe(expectedJoke.Id);
            searchResult!.IconUrl.ShouldBe(expectedJoke.Icon_Url);
            searchResult!.Url.ShouldBe(expectedJoke.Url);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("wertyuiuytrewsxcvjgusdciauksdhc akusdhbciwudhbckhducwodhcbwhubweucbkawduhcWKEUHBFVWuefvbwugevkwuagfwajrfvbwajcapworjvqpnjrqpc")]
        public async Task ItShouldNotSearchJoke(string query)
        {
            // Arrange
            var chuckService = Substitute.For<IChuckService>();

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
            var controller = new ChuckController(chuckService, _mapper);

            // Act
            var response = await controller.SearchJoke(query);

            // Assert
            var result = response.Result.ShouldBeAssignableTo<BadRequestObjectResult>();
            var errorMessage = result!.Value!.ToString();

            errorMessage!.ShouldContain("should be between 3 and 120");
        }
    }
}
