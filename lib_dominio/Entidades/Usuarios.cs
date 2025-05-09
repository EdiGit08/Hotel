using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Usuarios
    {
        public int Id { get; set; }
        public int Rol { get; set; }
        public string? Nombre { get; set; }
        public string? Contrasena { get; set; }
        [ForeignKey("Rol")] public Roles? _Rol { get; set; }

    }
}
