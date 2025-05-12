using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IRoles_PermisosPresentacion
    {
        Task<List<Roles_Permisos>> Listar();
        Task<List<Roles_Permisos>> PorCodigo(Roles_Permisos? entidad);
        Task<Roles_Permisos?> Guardar(Roles_Permisos? entidad);
        Task<Roles_Permisos?> Modificar(Roles_Permisos? entidad);
        Task<Roles_Permisos?> Borrar(Roles_Permisos? entidad);
    }
}