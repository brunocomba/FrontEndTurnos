using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.ApiService;
using MVC.Models.DTOs;
using MVC.Models;
using MVC.Services;
using MVC.Models.DTOs.Deportes;
using MVC.Models.DTOs.Canchas;

namespace MVC.Controllers
{
    public class CanchaController : Controller
    {
        private readonly DeporteService _deporteService;
        private readonly CanchaService _canchaService;


        public CanchaController(DeporteService deporteService,  CanchaService canchaService)
        {
            _deporteService = deporteService;
            _canchaService = canchaService;
        }

        // Rellenar los combos en el formulario de crerar nuevo turno
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            var deportes = await _deporteService.GetAllDeportesAsync();

            var viewModel = new AltaCanchaViewModel
            {

                ListaDeportes = deportes.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),  // Lo que devolverá al seleccionar
                    Text = d.Name             // Lo que mostrará en el combo
                }).ToList(),

            };

            return View(viewModel);

        }


        // crear
        [HttpPost]
        public async Task<IActionResult> Create(AltaCanchaViewModel viewModel)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            // Crear el DTO directamente
            AltaCanchaDTO dto = new AltaCanchaDTO
            {
                idDep = viewModel.deporteSeleccionado,
                Name = viewModel.Nombre,
                Precio = viewModel.Precio,
            };


            // Agregar el turno
            string response = await _canchaService.Add(dto);

            // Almacenar mensaje en TempData
            TempData["msj"] = response;

            return await Create(); // cargar los combos otra vez


            //return RedirectToAction("Index", "Home"); // Redirigir a una lista de turnos o página principal

        }


        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;
            var canchas = await _canchaService.GetAllCanchasAsync();

            // Mapea los turnos a TurnoViewModel
            var canchaViewModel = canchas.Select(c => new ListaCanchaViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Deporte = c.Deporte,
                cantJugadores = c.Deporte.cantJugadores, 
                Precio = c.Precio, 
            }).ToList();

            return View(canchaViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                string response = await _canchaService.DeleteCanchaAsync(id);

                if (response == "Cancha eliminado con exito")
                {
                    TempData["SuccessMessage"] = response;
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al intentar eliminar el cliente.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ocurrió un error: {ex.Message}";
            }

            return RedirectToAction("Listado"); // Redirige a la vista de la lista después de la eliminación
        }
    }
}
