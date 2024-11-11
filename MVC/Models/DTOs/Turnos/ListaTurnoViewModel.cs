namespace MVC.Models.DTOs
{
    public class ListaTurnoViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Horario { get; set; }
        public Cliente Cliente { get; set; }
        public uint TelefonoCliente { get; set; }
        public string Cancha { get; set; }
        public decimal Precio { get; set; }
    }
}
