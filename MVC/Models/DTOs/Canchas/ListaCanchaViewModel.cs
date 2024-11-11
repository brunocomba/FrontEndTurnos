namespace MVC.Models.DTOs.Canchas
{
    public class ListaCanchaViewModel
    {
        public int Id { get; set; }
        public Deporte Deporte { get; set; }
        public int cantJugadores { get; set; }
        public string Name { get; set; }
        public decimal Precio { get; set; }

    }
}
