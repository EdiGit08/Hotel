
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public int Opinion { get; set; }

        [ForeignKey("Opinion")] public Opiniones? _Opinion { get; set; }

    }
}
