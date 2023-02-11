namespace ChuckWarsApi.Shared.Models.Chuck
{
    public class ChuckSearchModel
    {
        public int Total { get; set; }

        public List<ChuckJokeModel> Result { get; set; } = new();
    }
}
