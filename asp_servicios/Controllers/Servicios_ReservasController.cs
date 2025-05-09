

using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Servicios_ReservasController
    {

        public int Id { get; set; }

        public int Servicio { get; set; }
        public int Reserva { get; set; }

        [ForeignKey("Servicio")] public ServiciosController? _Servicio { get; set; }
        [ForeignKey("Reserva")] public ReservasController? _Reserva { get; set; }

    }
}
