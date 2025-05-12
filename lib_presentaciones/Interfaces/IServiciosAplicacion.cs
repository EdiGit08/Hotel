using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IServiciosPresentacion
    {
        Task<List<Servicios>> Listar();
        Task<List<Servicios>> PorTipo(Servicios? entidad);
        Task<Servicios?> Guardar(Servicios? entidad);
        Task<Servicios?> Modificar(Servicios? entidad);
        Task<Servicios?> Borrar(Servicios? entidad);
    }
}