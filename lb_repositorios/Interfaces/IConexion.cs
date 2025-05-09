using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorios.Interfaces
{
    /* Esta interfaz IConexion define los métodos y propiedades básicos que cualquier clase
    de repositorio o clase de acceso a datos que se encargue de la conexión a la base de datos debería implementar.*/
    public interface IConexion
    {
        string? StringConexion { get; set; }

        public DbSet<Habitaciones>? Habitaciones { get; set; }
        public DbSet<Recepcionistas>? Recepcionistas { get; set; }
        public DbSet<Opiniones>? Opiniones { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Servicios>? Servicios { get; set; }
        public DbSet<Servicios_Reservas>? Servicios_Reservas { get; set; }
        public DbSet<Promociones>? Promociones { get; set; }
        public DbSet<Pagos>? Pagos { get; set; }
        public DbSet<Auditorias>? Auditorias { get; set; }
        public DbSet<AuditoriasPagos>? AuditoriasPagos { get; set; }
        public DbSet<AuditoriasReservas>? AuditoriasReservas { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Permisos>? Permisos { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Roles_Permisos>? Roles_Permisos { get; set; }

        //Permite obtener el estado de una entidad, es decir, saber si ha sido modificada, añadida o eliminada.
        EntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}