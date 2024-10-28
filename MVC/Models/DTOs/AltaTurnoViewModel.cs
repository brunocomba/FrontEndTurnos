using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs
{
    public class AltaTurnoViewModel
    {

        public List<SelectListItem> Clientes { get; set; }

        [Required]
        public int clienteSeleccionado { get; set; }
        public List<SelectListItem> Canchas { get; set; }

        [Required]
        public int canchaSeleccionada { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        public List<SelectListItem> ListaHorarios { get; set; }  

        [Required(ErrorMessage = "El horario es obligatorio")]
        public string horarioSelec { get; set; }
    }
}
