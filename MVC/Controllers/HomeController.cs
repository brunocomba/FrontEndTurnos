using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.DTOs;
using MVC.Services;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly TurnoService _turnoService;

        public HomeController(TurnoService turnoService)
        {
            _turnoService = turnoService;
        }



        public async Task<IActionResult> Index()
        {

            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName; 

            var fechaActual = DateTime.Now.Date; // Obtiene la fecha actual
            var turnos =  await _turnoService.TurnosDelDia(fechaActual); // Llama a tu servicio

            // Mapea los turnos a TurnoViewModel
            var turnosViewModel = turnos.Select(t => new ListaTurnoViewModel
            {
                Id = t.Id,
                Fecha = t.Fecha,
                Horario = t.Horario,
                Cliente = t.Cliente, // Asegúrate de que esto sea correcto
                TelefonoCliente = t.Cliente.Telefono, // Asegúrate de que esto sea correcto
                Cancha = t.Cancha.Name, // Asegúrate de que esto sea correcto
                Precio = t.Cancha.Precio // Asegúrate de que esto sea correcto
            }).ToList();

            return View(turnosViewModel);
        }



    }
}
