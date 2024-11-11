using MVC.Models;
using MVC.Models.DTOs;
using MVC.Models.DTOs.Canchas;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

        public async Task<string> Add(AltaCanchaDTO turno)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(turno);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"canchas/add", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;
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

        public async Task<string> DeleteCanchaAsync(int id)
        {
            // Obtiene el token de autenticación desde la sesión
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Construye la URL para la solicitud DELETE
            var requestUrl = $"canchas/delete{id}";

            // Realiza la solicitud DELETE
            var response = await _httpClient.DeleteAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;
        }
    }
}
