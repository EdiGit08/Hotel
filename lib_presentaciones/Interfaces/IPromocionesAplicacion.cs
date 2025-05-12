using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IPromocionesPresentacion
    {
        Task<List<Promociones>> Listar();
        Task<List<Promociones>> PorCodigo(Promociones? entidad);
        Task<Promociones?> Guardar(Promociones? entidad);
        Task<Promociones?> Modificar(Promociones? entidad);
        Task<Promociones?> Borrar(Promociones? entidad);
    }
}
