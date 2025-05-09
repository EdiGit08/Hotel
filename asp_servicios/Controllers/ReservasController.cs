

using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class ReservasController
    {

        public int Id { get; set; }
        public string? Codigo { get; set; }
        public DateTime Fecha_Entrada { get; set; }
        public DateTime Fecha_Salida { get; set; }

        public int Cliente { get; set; }
        public int Recepcionista { get; set; }
        public int Habitacion { get; set; }

        [ForeignKey("Recepcionista")] public RecepcionistasController? _Recepcionista { get; set; }
        [ForeignKey("Cliente")] public ClientesController? _Cliente { get; set; }
        [ForeignKey("Habitacion")] public HabitacionesController? _Habitacion { get; set; }

    }
}
