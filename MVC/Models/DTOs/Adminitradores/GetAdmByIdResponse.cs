namespace MVC.Models.DTOs.Adminitradores
{
    public class GetAdmByIdResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
