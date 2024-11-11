using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models.DTOs.Canchas
{
    public class AltaCanchaViewModel
    {
        public List<SelectListItem> ListaDeportes { get; set; }
        public int deporteSeleccionado{ get; set; }

        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        

    }
}
