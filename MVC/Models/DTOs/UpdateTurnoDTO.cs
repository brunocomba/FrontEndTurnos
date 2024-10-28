namespace MVC.Models.DTOs
{
    public class UpdateTurnoDTO
    {
        public int idTurnoMod { get; set; }
        public int idCanchaNew { get; set; }
        public int idClienteNew { get; set; }
        public DateTime fechaNew { get; set; }
        public string Horario { get; set; }
    }
}
