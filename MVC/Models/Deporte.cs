using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC.Models
{
    public class Deporte
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int cantJugadores { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
