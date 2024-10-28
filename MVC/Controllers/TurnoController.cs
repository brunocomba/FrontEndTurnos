using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.ApiService;
using MVC.Models;
using MVC.Models.DTOs;
using MVC.Services;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace MVC.Controllers
{
    public class TurnoController : Controller
    {
        private readonly AdministradorService _adminService;
        private readonly ClienteService _clienteService;
        private readonly CanchaService _canchaService;
        private readonly TurnoService _turnoService;


        public TurnoController(AdministradorService adminService, ClienteService clienteService, CanchaService canchaService, TurnoService turnoService)
        {
            _adminService = adminService;
            _clienteService = clienteService;
            _canchaService = canchaService;
            _turnoService = turnoService;
        }

        // Rellenar los combos en el formulario de crerar nuevo turno
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            var adms = await _adminService.GetAllAdministradoresAsync();
            var clientes = await _clienteService.GetAllClientesAsync();
            var canchas = await _canchaService.GetAllCanchasAsync();

            Horarios hr = new Horarios();
            var hoararios = hr.ObtenerLista();

            // Creando el ViewModel y mapeando los adms, clientes y canchas a SelectListItem
            var viewModel = new AltaTurnoViewModel
            {

                Clientes = clientes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),  // Lo que devolverá al seleccionar
                    Text = c.Nombre             // Lo que mostrará en el combo
                }).ToList(),

                Canchas = canchas.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),

                ListaHorarios = hoararios.Select(h => new SelectListItem
                {
                    Value = h,
                    Text = h
                }).ToList(),
            };

            return View(viewModel);

        }


        // crear
        [HttpPost]
        public async Task<IActionResult> Create(AltaTurnoViewModel viewModel)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;
            // Crear el DTO directamente
            TurnoDTO dto = new TurnoDTO
            {
                idCliente = viewModel.clienteSeleccionado,
                idCancha = viewModel.canchaSeleccionada,
                Fecha = viewModel.Fecha,
                Horario = viewModel.horarioSelec
            };


            // Agregar el turno
            string response = await _turnoService.AddTurno(dto);

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
            var turnos = await _turnoService.Listdo();

            // Mapea los turnos a TurnoViewModel
            var turnosViewModel = turnos.Select(t => new ListaTurnoViewModel
            {
                Fecha = t.Fecha,
                Horario = t.Horario,
                Cliente = t.Cliente, // Asegúrate de que esto sea correcto
                TelefonoCliente = t.Cliente.Telefono, // Asegúrate de que esto sea correcto
                Cancha = t.Cancha.Name, // Asegúrate de que esto sea correcto
                Precio = t.Cancha.Precio // Asegúrate de que esto sea correcto
            }).ToList();

            return View(turnosViewModel);
        }

        // Método para cargar los datos del formulario en caso de fallo
        private async Task CargarDatosParaFormulario(int idTurnoMod, TurnoEditDTO viewModel)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            // Aquí obtendrás los datos del turno, los clientes, horarios y canchas
            var turno = await _turnoService.GetById(idTurnoMod);
            viewModel.FechaSeleccionada = turno.Fecha.ToString("yyyy-MM-dd");
            viewModel.Horario = turno.Horario.ToString();   
            viewModel.ClienteSeleccionadoNombre = turno.Cliente.Nombre;
            viewModel.CanchaSeleccionadaNombre = turno.Cancha.Name;


            Horarios hr = new Horarios();
            var horarios = hr.ObtenerLista();

            // Recargar las listas de clientes, horarios y canchas para el dropdown
            var clientes = await _clienteService.GetAllClientesAsync();
            var canchas = await _canchaService.GetAllCanchasAsync();

            viewModel.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),        // Lo que devolverá al selecciona
                Text = c.Nombre,                   // Lo que mostrará en el combo
                Selected = c.Id == turno.Cliente.Id // Marca como seleccionado


            }).ToList();

            viewModel.Canchas = canchas.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == turno.Cancha.Id // Marca como seleccionado

            }).ToList();
               
            viewModel.Horarios = horarios.Select(h => new SelectListItem
            {
                Value = h,
                Text = h,

            }).ToList();




        }

        //private async Task<IEnumerable<TurnoEditDTO>> cargaViewModel(int id)
        //{
        //    var turno = await _turnoService.GetById(id); // Obtiene el turno por ID
        //    if (turno == null)
        //    {
        //        throw new Exception("Turno no encontrado");
        //    }

        //    var adms = await _adminService.GetAllAdministradoresAsync();
        //    var clientes = await _clienteService.GetAllClientesAsync();
        //    var canchas = await _canchaService.GetAllCanchasAsync();

        //    Horarios hr = new Horarios();
        //    var horarios = hr.ObtenerLista();

        //    // Carga los datos para los combos
        //    var viewModel = new TurnoEditDTO
        //    {
        //        idTurnoMod = turno.Id,
        //        CanchaSeleccionadaNombre = turno.Cancha.Name,
        //        ClienteSeleccionadoNombre = turno.Cliente.ToString(),
        //        Horario = turno.Horario.ToString(),
        //        FechaSeleccionada = turno.Fecha.ToString("yyyy-MM-dd"),


        //        Clientes = clientes.Select(c => new SelectListItem
        //        {
        //            Value = c.Id.ToString(),        // Lo que devolverá al selecciona
        //            Text = c.Nombre,                   // Lo que mostrará en el combo
        //            Selected = c.Id == turno.Cliente.Id // Marca como seleccionado


        //        }).ToList(),

        //        Canchas = canchas.Select(c => new SelectListItem
        //        {
        //            Value = c.Id.ToString(),
        //            Text = c.Name,
        //            Selected = c.Id == turno.Cancha.Id // Marca como seleccionado

        //        }).ToList(),

        //        Horarios = horarios.Select(h => new SelectListItem
        //        {
        //            Value = h,
        //            Text = h,

        //        }).ToList()
        //    };

        //    return (IEnumerable<TurnoEditDTO>)viewModel;

        //}




        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            var turno = await _turnoService.GetById(id); // Obtiene el turno por ID
            if (turno == null)
            {
                NotFound();
            }

            var adms = await _adminService.GetAllAdministradoresAsync();
            var clientes = await _clienteService.GetAllClientesAsync();
            var canchas = await _canchaService.GetAllCanchasAsync();

            Horarios hr = new Horarios();
            var horarios = hr.ObtenerLista();

            // Carga los datos para los combos
            var viewModel = new TurnoEditDTO
            {
                idTurnoMod = turno.Id,
                CanchaSeleccionadaNombre = turno.Cancha.Name,
                ClienteSeleccionadoNombre = turno.Cliente.ToString(),
                Horario = turno.Horario.ToString(),
                FechaSeleccionada = turno.Fecha.ToString("yyyy-MM-dd"),


                Clientes = clientes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),        // Lo que devolverá al selecciona
                    Text = c.Nombre,                   // Lo que mostrará en el combo
                    Selected = c.Id == turno.Cliente.Id // Marca como seleccionado


                }).ToList(),

                Canchas = canchas.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == turno.Cancha.Id // Marca como seleccionado

                }).ToList(),

                Horarios = horarios.Select(h => new SelectListItem
                {
                    Value = h,
                    Text = h,

                }).ToList()
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Editar(TurnoEditDTO viewModel)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            try
            {
                // Crear el DTO directamente
                UpdateTurnoDTO dto = new UpdateTurnoDTO
                {
                    idTurnoMod = viewModel.idTurnoMod,
                    idClienteNew = viewModel.ClienteSeleccionado,
                    idCanchaNew = viewModel.CanchaSeleccionada,
                    fechaNew = viewModel.Fecha,
                    Horario = viewModel.HorarioSeleccionado
                };

                // Agregar el turno
                string response = await _turnoService.Update(dto);

                if (response == "Turno actualizado con exito")
                {
                    // Almacenar mensaje de éxito en TempData
                    ViewBag.SuccessMessage = response;
                    return View(viewModel); // Redirigir tras éxito
                }
                else
                {
                    // Mensaje de error
                    ViewBag.ErrorMessage = response;

                    // Recargar los datos del turno en caso de error
                    await CargarDatosParaFormulario(viewModel.idTurnoMod, viewModel);

                }
                // Volver a la vista con los datos
                return View("Editar", viewModel);
            }
            catch (Exception ex)
            {
                // Enviar mensaje de error por excepción
                ViewBag.Error = ex.Message;
                return View("Editar", viewModel); // Devuelve el formulario con el mensaje de error

            }


        }


    }
}
