using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MVC.Models;
using MVC.Models.DTOs;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Services
{
    public class TurnoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public TurnoService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://backturnos.azurewebsites.net/"); // URL base de la API
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddTurno(TurnoDTO turno)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken"); // O HttpContextAccessor en ASP.NET Core

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(turno);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"turnos/add", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;
        }
        

        public async Task<IEnumerable<Turno>> TurnosDelDia(DateTime fechaDelDia)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var fechaFormat = fechaDelDia.ToString("yyyy-MM-dd");

            var response = await _httpClient.GetAsync($"turnos/filtrar/por/dia?fecha={fechaFormat}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var turnos = JsonConvert.DeserializeObject<IEnumerable<Turno>>(content);
                return turnos ?? Enumerable.Empty<Turno>(); ;
            }

            return Enumerable.Empty<Turno>();

        }



        public async Task<IEnumerable<Turno>> Listdo()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.GetAsync($"turnos/listado");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var turnos = JsonConvert.DeserializeObject<IEnumerable<Turno>>(content);
                return turnos ?? Enumerable.Empty<Turno>(); ;
            }

            return Enumerable.Empty<Turno>();

        }

        public async Task<string> Update(UpdateTurnoDTO dto)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"turnos/update", content);

            if (response.IsSuccessStatusCode)
            {
                var ok = await response.Content.ReadAsStringAsync();
                return ok;
            }

            var msj = await response.Content.ReadAsStringAsync();
            return msj;

        }

        public async Task<Turno>GetById(int idTurno)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Hace la solicitud GET a la API
            var response = await _httpClient.GetAsync($"turnos/buscar{idTurno}");

            if (response.IsSuccessStatusCode)
            {
                // Si la solicitud es exitosa, deserializa el contenido a un objeto Administrador
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var turno = JsonConvert.DeserializeObject<Turno>(jsonResponse);
                return turno;
            }

            return null;

        }
    }
    



}
