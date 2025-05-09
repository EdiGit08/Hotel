

using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class PagosController
    {

        public int Id { get; set; }
        public double Total { get; set; }
        public string? Medio { get; set; }

        public int Reserva { get; set; }
        public int Promocion { get; set; }

        [ForeignKey("Reserva")] public ReservasController? _Reserva { get; set; }
        [ForeignKey("Promocion")] public PromocionesController? _Promocion { get; set; }

    }
}
