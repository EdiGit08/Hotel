using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IOpinionesAplicacion
    {
        void Configurar(string StringConexion);
        List<Opiniones> PorOpcion(Opiniones? entidad);
        List<Opiniones> Listar();
        Opiniones? Guardar(Opiniones? entidad);
        Opiniones? Modificar(Opiniones? entidad);
        Opiniones? Borrar(Opiniones? entidad);
    }
}