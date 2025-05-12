using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IOpinionesPresentacion
    {
        Task<List<Opiniones>> Listar();
        Task<List<Opiniones>> PorOpcion(Opiniones? entidad);
        Task<Opiniones?> Guardar(Opiniones? entidad);
        Task<Opiniones?> Modificar(Opiniones? entidad);
        Task<Opiniones?> Borrar(Opiniones? entidad);
    }
}