using Microsoft.Extensions.Options;

namespace MVC.Models
{
    public class Horarios
    {

        // Lista privada y hardcodeada de horarios
        private readonly List<string> horarios = new List<string>
        {
            "16:00",
            "16:30",
            "17:00",
            "17:30",
            "18:00",
            "18:30",
            "19:00",
            "19:30",
            "20:00",
            "20:30",
            "21:00"
        };

        public List<string> ObtenerLista()
        {
            return horarios;
        }
    }
}
