using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC.ApiService;
using System.Security.Claims;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.DotNet.MSIdentity.Shared;
using NuGet.Protocol.Plugins;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService , IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://backturnos.azurewebsites.net/"); // URL base de la API
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var loginData = new { email, password };

            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("administradores/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);

                // Almacena el token en la sesión
                HttpContext.Session.SetString("AuthToken", tokenResponse.Token);

                HttpContext.Session.SetString("AdminName", tokenResponse.Admin);


                return RedirectToAction("Index", "Home");
            }
            
            var errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.Error = errorMessage;
            return View("Index", "Account");
        }

        [HttpPost]
        public ActionResult Logout()
        {

            // Eliminar toda la sesión si prefieres limpiar completamente el estado del usuario
            HttpContext.Session.Clear();

            // Redirigir al usuario al formulario de login
            return RedirectToAction("Index", "Account");
        }
    }

            
    
}
