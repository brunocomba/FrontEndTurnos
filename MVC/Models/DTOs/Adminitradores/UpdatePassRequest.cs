using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs.Adminitradores
{
    public class UpdatePassRequest
    {
        public int idAdmiMod { get; set; }

        [Display(Name = "Contraseña actual")]
        public string? passAntigua { get; set; }

        [Display(Name = "Contraseña nueva")]
        public string? passNew { get; set; }

        [Display(Name = "Confirmar contraseña nueva")]
        public string? confirPassNew { get; set; }
    }
}
