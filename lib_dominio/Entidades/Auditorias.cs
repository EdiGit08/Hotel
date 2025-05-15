using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Auditorias
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Accion { get; set; }
        public DateTime Fecha { get; set; }
    }
}