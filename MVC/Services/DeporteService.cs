using MVC.Models;
using MVC.Models.DTOs.Adminitradores;
using MVC.Models.DTOs.Deportes;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Services
{
    public class DeporteService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;


        public DeporteService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://backturnos.azurewebsites.net/"); // URL base de la API
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> Add(AltaDeporteDTO altaDep)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(altaDep);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"deportes/add", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;
        }


        public async Task<string> DeleteDeporteAsync(int id)
        {
            // Obtiene el token de autenticación desde la sesión
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Construye la URL para la solicitud DELETE
            var requestUrl = $"deportes/delete{id}";

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


        public async Task<string> Update(UpdateDeporteDTO updateDTO)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(updateDTO);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"deportes/update", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;

        }
        public async Task<Deporte> GetById(int idDep)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hace la solicitud GET a la API
            var response = await _httpClient.GetAsync($"deportes/buscar{idDep}");

            if (response.IsSuccessStatusCode)
            {
                // Si la solicitud es exitosa, deserializa el contenido a un objeto Administrador
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dep = JsonConvert.DeserializeObject<Deporte>(jsonResponse);
                return dep;
            }

            return null;

        }


        public async Task<IEnumerable<Deporte>> GetAllDeportesAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hacer la solicitud GET a la API
            var response = await _httpClient.GetAsync("deportes/listado");

            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta
                var content = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON a una lista de Administradores
                var deps = JsonConvert.DeserializeObject<IEnumerable<Deporte>>(content);

                return deps;
            }

            return [];
        }
    }
}
