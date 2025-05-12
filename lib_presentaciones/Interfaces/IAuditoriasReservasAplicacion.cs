using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAuditoriasReservasPresentacion
    {
        Task<List<AuditoriasReservas>> Listar();
        Task<List<AuditoriasReservas>> PorCodigo(AuditoriasReservas? entidad);
        Task<AuditoriasReservas?> Guardar(AuditoriasReservas? entidad);
        Task<AuditoriasReservas?> Modificar(AuditoriasReservas? entidad);
        Task<AuditoriasReservas?> Borrar(AuditoriasReservas? entidad);
    }
}
