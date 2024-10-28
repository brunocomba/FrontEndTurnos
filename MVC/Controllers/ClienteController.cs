using Microsoft.AspNetCore.Mvc;
using MVC.ApiService;
using MVC.Controllers.Generic;
using MVC.Models;
using MVC.Models.DTOs;
using MVC.Models.DTOs.Clientes;
using MVC.Services;

namespace MVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;
        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AltaClienteDTO altaDTO)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;
           
            string response = await _clienteService.AddCliente(altaDTO);

            // Almacenar mensaje en TempData
            TempData["msj"] = response;

            return View(); // cargar los combos otra vez
        }

        [HttpGet]
        public async Task<IActionResult> Listado(int dniCliente)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            var clientes = await _clienteService.GetAllClientesAsync();

            if (dniCliente > 0)
            {
                clientes = (IEnumerable<Cliente>)_clienteService.GetByDNI(dniCliente);
            }

            // Mapea los turnos a TurnoViewModel
            var clientesViewModel = clientes.Select(c => new ListaClienteViewModel
            {
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Dni = c.Dni, // Asegúrate de que esto sea correcto
                Calle = c.Calle, // Asegúrate de que esto sea correcto
                Altura = c.Altura, // Asegúrate de que esto sea correcto
                Telefono = c.Telefono // Asegúrate de que esto sea correcto
            }).ToList();

            return View(clientesViewModel);
        }



    }
}
