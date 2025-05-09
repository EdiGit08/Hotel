
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class AuditoriasPrueba
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Accion { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("Opinion")] public Opiniones? _Opinion { get; set; }

    }
}
}
