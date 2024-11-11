using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs.Adminitradores
{
    public class UpdateDatosPerRequest
    {
        public int idAdmiMod { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime fechaNacimiento { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
    }
}
