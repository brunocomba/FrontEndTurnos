using Microsoft.AspNetCore.Mvc;
using MVC.ApiService;
using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace MVC.Controllers.Generic
{
    public class GenericController<T> : Controller where T : class
    {
        private readonly IApiClient<T> _apiClient;
        private readonly string _endpoint;

        public GenericController(IApiClient<T> apiClient, string endpoint)
        {
            _apiClient = apiClient;
            _endpoint = endpoint;
        }

        public async Task<IActionResult> Index(int dni)
        {

            if (dni != 0)
            {
                var item = await _apiClient.GetByIdAsync($"{_endpoint}", dni);
                return View(new List<T>([item]));

            }
            else
            {
                var items = await _apiClient.GetAllAsync(_endpoint);
                return View(items);

            }   
            

        }
    }
}
