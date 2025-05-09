using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IPermisosAplicacion
    {
        void Configurar(string StringConexion);
        List<Permisos> PorTipo(Permisos? entidad);
        List<Permisos> Listar();
        Permisos? Guardar(Permisos? entidad);
        Permisos? Modificar(Permisos? entidad);
        Permisos? Borrar(Permisos? entidad);
    }
}