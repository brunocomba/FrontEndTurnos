using MVC.Models;
using MVC.Models.DTOs;
using MVC.Models.DTOs.Clientes;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClienteService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://backturnos.azurewebsites.net/"); // URL base de la API
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> AddCliente(AltaClienteDTO clienteDTO)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(clienteDTO);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"clientes/add", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;
        }


        public async Task<string> Update(UpdateClienteDTO clienteDTO)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(clienteDTO);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"clientes/update", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;

        }

        public async Task<Cliente> GetById(int idCliente)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hace la solicitud GET a la API
            var response = await _httpClient.GetAsync($"clientes/buscar{idCliente}");

            if (response.IsSuccessStatusCode)
            {
                // Si la solicitud es exitosa, deserializa el contenido a un objeto Administrador
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(jsonResponse);
                return cliente ;
            }

            return null;

        }


        public async Task<Cliente> GetByDNI(int dniCliente)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hace la solicitud GET a la API
            var response = await _httpClient.GetAsync($"clientes/buscar/porDni{dniCliente}");

            if (response.IsSuccessStatusCode)
            {
                // Si la solicitud es exitosa, deserializa el contenido a un objeto Administrador
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(jsonResponse);
                return cliente;
            }

            return null;

        }


        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hacer la solicitud GET a la API
            var response = await _httpClient.GetAsync("clientes/listado");

            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta
                var content = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON a una lista de Administradores
                var adms = JsonConvert.DeserializeObject<IEnumerable<Cliente>>(content);

                return adms;
            }

            return [];
        }
    }
}
