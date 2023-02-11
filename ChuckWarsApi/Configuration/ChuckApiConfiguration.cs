namespace ChuckWarsApi.Configuration
#pragma warning disable CS1591
{
    public class ChuckApiConfiguration
    {
        public static string SectionName => "ChuckNorrisApi";

        public string BaseUrl { get; set; } = null!;
    }
#pragma warning restore CS1591
}
