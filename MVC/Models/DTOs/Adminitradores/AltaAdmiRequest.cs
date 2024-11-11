using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs.Adminitradores
{
    public class AltaAdmiRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime fechaNacimiento { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
        public string Email { get; set; }
        public string confirEmail { get; set; }
        public string Password { get; set; }
        public string confirPass { get; set; }
    }
}
