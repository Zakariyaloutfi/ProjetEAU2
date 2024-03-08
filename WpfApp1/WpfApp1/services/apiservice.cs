using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using WpfApp;

public class ApiService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public ApiService()
    {
        _httpClient.BaseAddress = new System.Uri("https://localhost:44356/api/ReservesEau");

        public async Task<List<Kc>> GetKcsAsync()
        {
            var response = await _httpClient.GetAsync("api/Kc");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Kc>>(content);
        }

        public async Task<ReserveEau> GetReserveEauByCodePostalAsync(string codePostal)
        {
            var response = await _httpClient.GetAsync($"api/ReservesEau/{codePostal}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReserveEau>(content);
        }

        public async Task CreateReserveEauAsync(ReserveEau reserveEau)
        {
            var content = new StringContent(JsonConvert.SerializeObject(reserveEau), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/ReservesEau", content);
            response.EnsureSuccessStatusCode();
        }
    }
