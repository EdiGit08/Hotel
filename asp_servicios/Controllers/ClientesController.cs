
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class ClientesController
    {

        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public int Opinion { get; set; }

        [ForeignKey("Opinion")] public OpinionesController? _Opinion { get; set; }

    }
}
