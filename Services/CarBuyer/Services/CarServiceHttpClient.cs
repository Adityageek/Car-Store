using CarBuyer.Data;

namespace CarBuyer.Services
{
    public class CarServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public CarServiceHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<Car>> GetCarsForSearchDb()
        {
            return await _httpClient.GetFromJsonAsync<List<Car>>(_config["CarServiceUrl"]
                + "/api/car");
        }
    }
}
