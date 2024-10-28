using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MVC.Services
{
    public class CanchaService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CanchaService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://backturnos.azurewebsites.net/"); // URL base de la API
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Cancha>> GetAllCanchasAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hacer la solicitud GET a la API
            var response = await _httpClient.GetAsync("canchas/listado");

            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta
                var content = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON a una lista de Administradores
                var adms = JsonConvert.DeserializeObject<IEnumerable<Cancha>>(content);

                return adms;
            }

            return [];
        }
    }
}
