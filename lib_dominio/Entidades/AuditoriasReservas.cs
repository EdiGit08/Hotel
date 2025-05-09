using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class AuditoriasReservas : Auditorias
    {
        public int Reserva;
        [ForeignKey("Reserva")] public Reservas? _Reserva { get; set; }
    }
}