using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IRoles_PermisosAplicacion
    {
        void Configurar(string StringConexion);
        List<Roles_Permisos> PorCodigo(Roles_Permisos? entidad);
        List<Roles_Permisos> Listar();
        Roles_Permisos? Guardar(Roles_Permisos? entidad);
        Roles_Permisos? Modificar(Roles_Permisos? entidad);
        Roles_Permisos? Borrar(Roles_Permisos? entidad);
    }
}