using System.Xml.Linq;

namespace MVC.Models
{
    public class Administrador
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
        public string confirPass { get; set; }


        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
