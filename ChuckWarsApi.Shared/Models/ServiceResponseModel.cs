namespace ChuckWarsApi.Shared.Models
{
    public class ServiceResponseModel<T>
        where T : class
    {
        public ServiceResponseModel()
        {
        }

        public ServiceResponseModel(bool isSuccessful, string? errorMessage)
        {
            IsSuccessful= isSuccessful;
            ErrorMessage= errorMessage;
        }

        public bool IsSuccessful { get; set; }

        public string? ErrorMessage { get; set; }

        public T? Data { get; set; }
    }
}
