using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class AuditoriasPagos : Auditorias
    {
        public int Pago;
        [ForeignKey("Pago")] public Pagos? _Pago { get; set; }
    }
}