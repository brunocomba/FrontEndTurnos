using Newtonsoft.Json;
using System.Text;

namespace MVC.ApiService
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://backturnos.azurewebsites.net/"); // URL base de la API

        }

        public async Task<HttpResponseMessage> LoginAsync(string email, string password)
        {
            var loginData = new { email, password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("administradores/login", content);

            return response;
          
        }
    }
}
