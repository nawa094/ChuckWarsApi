using ChuckWarsApi.Services.Chuck;
using ChuckWarsApi.Shared.Dtos.Chuck;
using NSubstitute;
using RichardSzalay.MockHttp;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace ChuckWarsApi.Tests.Services.Chuck
{
    public class ChuckServiceTests
    {
        private readonly MockHttpMessageHandler _handler = new();

        [Fact]
        public async Task ItShouldGetCategories()
        {
            // Arrange
            var category = new List<string>
            {
                "batman",
                "spiderman "
            };

            _handler.When("https://api.chucknorris.io/jokes/categories").Respond(HttpStatusCode.OK, JsonContent.Create(category));

            var client = new HttpClient(_handler)
            {
                 BaseAddress = new Uri("https://api.chucknorris.io/jokes/")
            };

            var service = new ChuckService(client);

            // Act
            var result = await service.GetCategoriesAsync();

            // Assert
            result.Categories.ShouldBe(category);
        }

        [Fact]
        public async Task ItShouldGetJoke()
        {
            // Arrange
            var category = "batman";
            var expectedJokeDto = new JokeDto
            {
                Icon_Url = "icon",
                Id = "2",
                Url = "url",
                Value = "value"
            };

            _handler.When($"https://api.chucknorris.io/jokes/random?category={category}")
                .Respond(HttpStatusCode.OK, JsonContent.Create(expectedJokeDto));

            var client = new HttpClient(_handler)
            {
                BaseAddress = new Uri("https://api.chucknorris.io/jokes/")
            };

            var service = new ChuckService(client);

            // Act
            var result = await service.GetJokeAsync(category);

            // Assert
            result!.Id.ShouldBe(expectedJokeDto.Id);
            result!.Icon_Url.ShouldBe(expectedJokeDto.Icon_Url);
            result!.Url.ShouldBe(expectedJokeDto.Url);
            result!.Value.ShouldBe(expectedJokeDto.Value);
        }

        [Fact]
        public async Task ItShouldSearchJoke()
        {
            // Arrange
            var query = "batman";

            var expectedJoke = new JokeDto
            {
                Icon_Url = "icon",
                Id = "2",
                Url = "url",
                Value = "value"
            };

            var expectedSearchDto = new SearchDto
            {
                Total = 1,
                Result = new List<JokeDto> { expectedJoke }
            };

            _handler.When($"https://api.chucknorris.io/jokes/search?query={query}")
                .Respond(HttpStatusCode.OK, JsonContent.Create(expectedSearchDto));

            var client = new HttpClient(_handler)
            {
                BaseAddress = new Uri("https://api.chucknorris.io/jokes/")
            };

            var service = new ChuckService(client);

            // Act
            var result = await service.SearchJokeAsync(query);

            // Assert
            result.Total.ShouldBe(1);

            var actualResult = result.Result.Single();

            actualResult!.Id.ShouldBe(expectedJoke.Id);
            actualResult!.Icon_Url.ShouldBe(expectedJoke.Icon_Url);
            actualResult!.Url.ShouldBe(expectedJoke.Url);
            actualResult!.Value.ShouldBe(expectedJoke.Value);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("wertyuiuytrewsxcvjgusdciauksdhc akusdhbciwudhbckhducwodhcbwhubweucbkawduhcWKEUHBFVWuefvbwugevkwuagfwajrfvbwajcapworjvqpnjrqpc")]
        public async Task ItShouldNotSearchJoke(string query)
        {
            // Arrange
            var expectedJoke = new JokeDto
            {
                Icon_Url = "icon",
                Id = "2",
                Url = "url",
                Value = "value"
            };

            var expectedSearchDto = new SearchDto
            {
                Total = 1,
                Result = new List<JokeDto> { expectedJoke }
            };

            _handler.When($"https://api.chucknorris.io/jokes/search?query={query}")
                .Respond(HttpStatusCode.OK, JsonContent.Create(expectedSearchDto));

            var client = new HttpClient(_handler)
            {
                BaseAddress = new Uri("https://api.chucknorris.io/jokes/")
            };

            var service = new ChuckService(client);

            // Act
            var result = await service.SearchJokeAsync(query);

            // Assert
            result.ShouldBeNull();
        }
    }
}
