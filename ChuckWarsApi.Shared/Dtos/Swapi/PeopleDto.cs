using ChuckWarsApi.Shared.Models.Swapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckWarsApi.Shared.Dtos.Swapi
{
    public class PeopleDto
    {
        public int Count { get; set; }

        public string? Next { get; set; }

        public string? Previous { get; set; }

        public List<PersonDto> Results { get; set; } = new();
    }
}
