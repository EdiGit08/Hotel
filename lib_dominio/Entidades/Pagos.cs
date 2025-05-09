using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Pagos
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public double Total { get; set; }
        public string? Medio { get; set; }

        public int Reserva { get; set; }
        public int Promocion { get; set; }

        [ForeignKey("Reserva")] public Reservas? _Reserva { get; set; }
        [ForeignKey("Promocion")] public Promociones? _Promocion { get; set; }

    }
}
