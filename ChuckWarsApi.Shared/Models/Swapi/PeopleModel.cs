namespace ChuckWarsApi.Shared.Models.Swapi
{
    public class PeopleModel
    {
        public int Count { get; set; }  

        public string? Next { get; set; }

        public string? Previous { get; set; }

        public List<PersonModel> Results { get; set; } = new(); 
    }
}
