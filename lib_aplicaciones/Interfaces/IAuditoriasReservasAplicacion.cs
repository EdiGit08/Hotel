using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IAuditoriasReservasAplicacion
    {
        void Configurar(string StringConexion);
        List<AuditoriasReservas> PorCodigo(AuditoriasReservas? entidad);
        List<AuditoriasReservas> Listar();
        AuditoriasReservas? Guardar(AuditoriasReservas? entidad);
        AuditoriasReservas? Modificar(AuditoriasReservas? entidad);
        AuditoriasReservas? Borrar(AuditoriasReservas? entidad);
    }
}
