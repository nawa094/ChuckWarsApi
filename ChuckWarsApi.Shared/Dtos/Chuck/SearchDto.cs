namespace ChuckWarsApi.Shared.Dtos.Chuck
{
    public class SearchDto
    {
        public int Total { get; set; }

        public List<JokeDto> Result { get; set; } = new();
    }
}
