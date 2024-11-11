using Microsoft.AspNetCore.Mvc;
using MVC.Models;
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

            if (dniCliente > 0)
            {
                // Busca el cliente específico por DNI
                Cliente cliente = await _clienteService.GetByDNI(dniCliente);

                if (cliente != null)
                {
                    // Mapea el cliente encontrado a un ViewModel y devuelve una lista con un solo elemento
                    var clienteViewModel = new List<ListaClienteViewModel>
                    {
                        new ListaClienteViewModel
                        {
                            Id = cliente.Id,
                            Nombre = cliente.Nombre,
                            Apellido = cliente.Apellido,
                            Dni = cliente.Dni,
                            fechaNacimiento = cliente.fechaNacimiento,
                            Calle = cliente.Calle,
                            Altura = cliente.Altura,
                            Telefono = cliente.Telefono
                        }
                    };
                    return View(clienteViewModel);

                }
                // Si no se encuentra el cliente, puedes manejarlo de varias maneras
                ViewBag.Error = ("", "Cliente no encontrado");
                return View(new List<ListaClienteViewModel>()); // O devuelve una lista vacía
            }

            // Si no se busca por DNI, devuelve todos los clientes
            var clientes = await _clienteService.GetAllClientesAsync();
            var clientesViewModel = clientes.Select(c => new ListaClienteViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Dni = c.Dni,
                fechaNacimiento = c.fechaNacimiento,
                Calle = c.Calle,
                Altura = c.Altura,
                Telefono = c.Telefono
            }).ToList();

            return View(clientesViewModel);
        }


        // Método para cargar los datos del formulario en caso de fallo
        private async Task CargarDatosParaFormulario(int idClienteMod, ClienteEditViewModel viewModel)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            // Aquí obtendrás los datos del turno, los clientes, horarios y canchas
            var cliente = await _clienteService.GetById(idClienteMod);
            viewModel.Nombre = cliente.Nombre;
            viewModel.Apellido = cliente.Apellido;
            viewModel.Dni = cliente.Dni;
            viewModel.fechaNacimiento = cliente.fechaNacimiento;
            viewModel.Calle = cliente.Calle;
            viewModel.Altura = cliente.Altura;  
            viewModel.Telefono = cliente.Telefono;  

        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            var cliente = await _clienteService.GetById(id); // Obtiene el turno por ID
            if (cliente == null)
            {
                NotFound();
            }

            // Carga los datos para los combos
            var viewModel = new ClienteEditViewModel
            {
                idClienteSelec = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Dni = cliente.Dni,
                fechaNacimiento = cliente.fechaNacimiento.Date,
                Calle = cliente.Calle,
                Altura = cliente.Altura,
                Telefono = cliente.Telefono
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Editar(ClienteEditViewModel viewModel)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            try
            {
                // Crear el DTO directamente
                UpdateClienteDTO dto = new UpdateClienteDTO
                {
                    idCliMod = viewModel.idClienteSelec,
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    Dni = viewModel.Dni,
                    fechaNacimiento = viewModel.fechaNacimiento,
                    Calle = viewModel.Calle,
                    Altura = viewModel.Altura,
                    Telefono = viewModel.Telefono
                };

                // Agregar el turno
                string response = await _clienteService.Update(dto);

                if (response == "Cliente actualizado con exito")
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
                    await CargarDatosParaFormulario(viewModel.idClienteSelec, viewModel);

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

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                string response = await _clienteService.DeleteClienteAsync(id);

                if (response == "Cliente eliminado con exito")
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
