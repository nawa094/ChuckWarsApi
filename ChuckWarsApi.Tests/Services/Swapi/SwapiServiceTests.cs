using RichardSzalay.MockHttp;
using System.Net.Http.Json;
using System.Net;
using Xunit;
using ChuckWarsApi.Services.Swapi;
using ChuckWarsApi.Shared.Dtos.Swapi;
using Shouldly;

namespace ChuckWarsApi.Tests.Services.Swapi
{
    public class SwapiServiceTests
    {
        private readonly MockHttpMessageHandler _handler = new();

        [Fact]
        public async Task ItShouldGetPeople()
        {
            // Arrange
            var person = new PersonDto
            {
                Name = "Test",
                Mass = "light"
            };

            var people = new PeopleDto
            {
                Count = 1,
                Next = "?page=2",
                Results = new List<PersonDto> { person }
            };

            _handler.When("https://swapi.dev/api/people").Respond(HttpStatusCode.OK, JsonContent.Create(people));

            var client = new HttpClient(_handler)
            {
                BaseAddress = new Uri("https://swapi.dev/api/")
            };

            var service = new SwapiService(client);

            // Act
            var result = await service.GetPeopleAsync(1);

            // Assert
            result.Count.ShouldBe(1);
            result.Next.ShouldBe(people.Next);
            var actualPerson = result.Results.Single();

            person.Name.ShouldBe(person.Name);
            person.Mass.ShouldBe(person.Mass);
        }

        [Fact]
        public async Task ItShouldSearchPerson()
        {
            // Arrange
            var query = "batman";

            var person = new PersonDto
            {
                Name = "Test",
                Mass = "light"
            };

            var people = new PeopleDto
            {
                Count = 1,
                Next = "?page=2",
                Results = new List<PersonDto> { person }
            };

            _handler.When($"https://swapi.dev/api/people?search={query}").Respond(HttpStatusCode.OK, JsonContent.Create(people));

            var client = new HttpClient(_handler)
            {
                BaseAddress = new Uri("https://swapi.dev/api/")
            };

            var service = new SwapiService(client);

            // Act
            var result = await service.SearchPeople(query);

            // Assert
            result.Count.ShouldBe(1);
            result.Next.ShouldBe(people.Next);
            var actualPerson = result.Results.Single();

            actualPerson.Name.ShouldBe(person.Name);
            actualPerson.Mass.ShouldBe(person.Mass);
        }
    }
}
