namespace MVC.Models.DTOs.Clientes
{
    public class ListaClienteViewModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int Dni { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string? Calle { get; set; }
        public int? Altura { get; set; }
        public uint Telefono { get; set; }


    }
}
