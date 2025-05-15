using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    // Se encarga de gestionar la conexión a la base de datos utilizando Entity Framework Core
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        //  Este método se utiliza para configurar cómo se conectará EF Core a la base de datos.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }


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
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Permisos>? Permisos { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Roles_Permisos>? Roles_Permisos { get; set; }
    }
}
