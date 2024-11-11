using Microsoft.AspNetCore.Mvc;
using MVC.ApiService;
using MVC.Models.DTOs.Adminitradores;
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


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AltaAdmiRequest altaReq)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            string response = await _administradorService.AddAdmi(altaReq);

            // Almacenar mensaje en TempData
            TempData["msj"] = response;

            return View(); // cargar los combos otra vez
        }




        // Método para cargar los datos del formulario en caso de fallo
        private async Task DatosFormularioUpdateDatosPer(int idAdmiMod, GetAdmByIdResponse viewModel)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            var adm = await _administradorService.GetById(idAdmiMod);

            viewModel.Nombre = adm.Nombre;
            viewModel.Apellido = adm.Apellido;
            viewModel.Dni = adm.Dni;
            viewModel.fechaNacimiento = adm.fechaNacimiento;
            viewModel.Calle = adm.Calle;
            viewModel.Altura = adm.Altura;

        }


        [HttpGet]
        public async Task<IActionResult> UpdateDatosPersonales(int id)
        {

            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            var adm = await _administradorService.GetById(id); // Obtiene el turno por ID
            if (adm == null)
            {
                NotFound();
            }

            var viewModel = new GetAdmByIdResponse
            {
                Id = adm.Id,
                Nombre = adm.Nombre,
                Apellido = adm.Apellido,
                Dni = adm.Dni,
                fechaNacimiento = adm.fechaNacimiento.Date,
                Calle = adm.Calle,
                Altura = adm.Altura,
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateDatosPersonales(GetAdmByIdResponse viewModel)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            try
            {
                // Crear el DTO directamente
                UpdateDatosPerRequest req = new UpdateDatosPerRequest
                {
                    idAdmiMod = viewModel.Id,
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    Dni = viewModel.Dni,
                    fechaNacimiento = viewModel.fechaNacimiento,
                    Calle = viewModel.Calle,
                    Altura = viewModel.Altura                
                };

                string response = await _administradorService.UpdateDatosPersonales(req);

                if (response == "Administrador actualizado con éxito")
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
                    await DatosFormularioUpdateDatosPer(viewModel.Id, viewModel);

                }
                // Volver a la vista con los datos
                return View("UpdateDatosPersonales", viewModel);
            }
            catch (Exception ex)
            {
                // Enviar mensaje de error por excepción
                ViewBag.Error = ex.Message;
                return View("UpdateDatosPersonales", viewModel); // Devuelve el formulario con el mensaje de error

            }
        }





        [HttpGet]
        public IActionResult UpdatePass()
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;

            var adminId = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminId = adminId;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePass(UpdatePassRequest req)
        {
            var adminName = HttpContext.Session.GetString("AdminName");
            ViewBag.Admin = adminName;
         


            try
            {
              
                string response = await _administradorService.UpdatePassword(req);

                if (response == "Administrador actualizado con éxito")
                {
                    // Almacenar mensaje de éxito en TempData
                    ViewBag.SuccessMessage = response;
                    return View(req); // Redirigir tras éxito
                }
                else
                {
                    // Mensaje de error
                    ViewBag.ErrorMessage = response;

                }
                // Volver a la vista con los datos
                return View("UpdatePass", req);
            }
            catch (Exception ex)
            {
                // Enviar mensaje de error por excepción
                ViewBag.Error = ex.Message;
                return View("UpdatePass", req); // Devuelve el formulario con el mensaje de error

            }
        }

    }
}
