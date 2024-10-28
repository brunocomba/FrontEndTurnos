using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs
{
    public class TurnoEditDTO
    {
        public int idTurnoMod { get; set; }
        // Para las selecciones

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string HorarioSeleccionado { get; set; }
        [Required]
        public int ClienteSeleccionado { get; set; }
        [Required]
        public int CanchaSeleccionada { get; set; }

        // Nombres que se mostrarán en el formulario solo lectura
        public string FechaSeleccionada { get; set; }
        public string Horario { get; set; }
        public string ClienteSeleccionadoNombre { get; set; }
        public string CanchaSeleccionadaNombre { get; set; }

        public List<SelectListItem> Horarios { get; set; }
        public List<SelectListItem> Clientes { get; set; }
        public List<SelectListItem> Canchas { get; set; }

    }
}
