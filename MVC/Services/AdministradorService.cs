using MVC.Models;
using MVC.Models.DTOs.Adminitradores;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.ApiService
{
    public class AdministradorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;


        public AdministradorService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://backturnos.azurewebsites.net/"); // URL base de la API
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> AddAdmi(AltaAdmiRequest altaReq)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(altaReq);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"administradores/add", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;
        }


        public async Task<string> UpdateDatosPersonales(UpdateDatosPerRequest updateDatosPersonalesReq)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(updateDatosPersonalesReq);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"administradores/update/datospersonales", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;

        }


        public async Task<string> UpdatePassword(UpdatePassRequest updatePassReq)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(updatePassReq);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"administradores/update/password", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;

        }



        public async Task<Administrador> GetById(int idAdmi)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hace la solicitud GET a la API
            var response = await _httpClient.GetAsync($"administradores/buscar{idAdmi}");

            if (response.IsSuccessStatusCode)
            {
                // Si la solicitud es exitosa, deserializa el contenido a un objeto Administrador
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var adm = JsonConvert.DeserializeObject<Administrador>(jsonResponse);
                return adm;
            }

            return null;

        }


        public async Task<IEnumerable<Administrador>> GetAllAdministradoresAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hacer la solicitud GET a la API
            var response = await _httpClient.GetAsync("administradores/listado");

            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta
                var content = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON a una lista de Administradores
                var adms = JsonConvert.DeserializeObject<IEnumerable<Administrador>>(content);

                return adms;
            }

            return [];
        }
    }
}
