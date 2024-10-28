namespace MVC.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Horario { get; set; }
        public Administrador Administrador { get; set; }
        public Cliente Cliente { get; set; }
        public Cancha Cancha { get; set; }
    }
}
