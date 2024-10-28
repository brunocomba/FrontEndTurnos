using Microsoft.AspNetCore.Mvc;
using MVC.ApiService;
using MVC.Controllers.Generic;
using MVC.Models;
using Newtonsoft.Json;
using System.Text;



namespace MVC.Controllers
{
    public class AdministradorController : Controller
    {

        private readonly AdministradorService _administradorService;

        public AdministradorController(AdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

       
        


        //public async Task<IActionResult> Index(int dni)
        //{
        //    if (dni != 0)
        //    {
        //        var resp = await _httpClient.GetAsync($"administradores/buscar/porDni{dni}");

        //        if (resp.IsSuccessStatusCode)
        //        {
        //            var content = await resp.Content.ReadAsStringAsync();
        //            var administrador = JsonConvert.DeserializeObject<Administrador>(content);



        //            return View("Index", new List<Administrador>([administrador]));
        //        }

        //    }
        //    else
        //    {
        //        var response = await _httpClient.GetAsync("administradores/listado");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            var administradores = JsonConvert.DeserializeObject<IEnumerable<Administrador>>(content);


        //            return View("Index", administradores);
        //        }

        //    }

        //    return View(new List<Administrador>());
        //}



        //[HttpGet]
        //public async Task<IActionResult> Editar(int idAdmiMod)
        //{
        //    Administrador administrador;

        //    try
        //    {


        //        // Hace la solicitud GET a la API
        //        var response = await _httpClient.GetAsync($"administradores/buscar{idAdmiMod}");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Si la solicitud es exitosa, deserializa el contenido a un objeto Administrador
        //            var jsonResponse = await response.Content.ReadAsStringAsync();
        //            administrador = JsonConvert.DeserializeObject<Administrador>(jsonResponse);
        //        }
        //        else
        //        {
        //            // Manejo de errores si la solicitud no es exitosa
        //            return BadRequest("Error al obtener el administrador desde la API.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones
        //        return BadRequest(ex.Message);
        //    }

        //    // Retorna la vista de modificación con el modelo del administrador
        //    return View(administrador);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(Administrador adm)
        //{

        //    var jsonData = JsonConvert.SerializeObject(adm);

        //    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PutAsync($"administradores/update/datosPersonales{adm.Id}", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Manejar la respuesta exitosa
        //        var result = await response.Content.ReadAsStringAsync();
        //        return Content($"Éxito: {result}");

        //        //return RedirectToAction("Index");   // volver al index
        //    }
        //    else
        //    {
        //        var resultNeg = await response.Content.ReadAsStringAsync();
        //        return Content($"Error: {resultNeg}");


        //    }

            //ModelState.AddModelError(string.Empty, "Error al crear un nuevo administrador.");

            //if (ModelState.IsValid)
            //{
            //    try
            //    {


            //        // Crear el contenido para el cuerpo de la solicitud usando directamente el objeto administrador
            //        var content = new StringContent(JsonConvert.SerializeObject(administrador), Encoding.UTF8, "application/json");

            //        // Hace la solicitud PUT a la API
            //        var response = await _httpClient.PostAsync("https://localhost:7147/administradores/update/datosPersonales", content);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            return RedirectToAction("Index");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError(string.Empty, "Error al actualizar los datos del administrador.");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
            //    }
            //}

            //return View(administrador);
        //}


        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Administrador adm)
        //{
        //    var jsonData = JsonConvert.SerializeObject(adm);

        //    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync("https://localhost:7147/administradores/add", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Manejar la respuesta exitosa
        //        var result = await response.Content.ReadAsStringAsync();
        //        return Content($"Éxito: {result}");

        //        //return RedirectToAction("Index");   // volver al index
        //    }
        //    else
        //    {
        //        var resultNeg = await response.Content.ReadAsStringAsync();
        //        return Content($"Error: {resultNeg}");

        //        //ModelState.AddModelError(string.Empty, "Error al crear un nuevo administrador.");
        //    }

        //}

    }
}
