using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Roles_Permisos
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public int Permiso { get; set; }
        public int Rol { get; set; }

        [ForeignKey("Permiso")] public Permisos? _Permiso { get; set; }
        [ForeignKey("Rol")] public Roles? _Rol { get; set; }
    }
}
