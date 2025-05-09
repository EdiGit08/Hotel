using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Servicios_Reservas
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public int Servicio { get; set; }
        public int Reserva { get; set; }

        [ForeignKey("Servicio")] public Servicios? _Servicio { get; set; }
        [ForeignKey("Reserva")] public Reservas? _Reserva { get; set; }
    }
}
