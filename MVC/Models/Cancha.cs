namespace MVC.Models
{
    public class Cancha
    {
        public int Id { get; set; }
        public Deporte Deporte { get; set; }
        public string Name { get; set; }
        public decimal Precio { get; set; }
    }
}
