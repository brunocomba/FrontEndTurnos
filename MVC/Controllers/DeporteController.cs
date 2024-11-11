using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs.Deportes;
using MVC.Services;

namespace MVC.Controllers
{
    public class DeporteController : Controller
    {
        private readonly DeporteService _deporteService;
        private readonly CanchaService _canchaService;

        public DeporteController(DeporteService deporteService, CanchaService canchaService)
        {
            _deporteService = deporteService;
            _canchaService = canchaService;
        }


        public IActionResult Create()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Create(AltaDeporteDTO altaDTO)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            string response = await _deporteService.Add(altaDTO);

            // Almacenar mensaje en TempData
            TempData["msj"] = response;

            return View(); // cargar los combos otra vez
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;



            // Si no se busca por DNI, devuelve todos los clientes
            var deportes = await _deporteService.GetAllDeportesAsync();
            var depViewModel = deportes.Select(d => new ListaDeporteViewModel
            {
                Id = d.Id,
                Nombre = d.Name,
                cantJugadores = d.cantJugadores,
            }).ToList();

            return View(depViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                string response = await _deporteService.DeleteDeporteAsync(id);

                if (response == "Deporte eliminado con exito")
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
