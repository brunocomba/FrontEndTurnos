using Microsoft.AspNetCore.Http;
using MVC.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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

        public async Task<IEnumerable<Administrador>> GetAllAdministradoresAsync()
        {

            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); 

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
