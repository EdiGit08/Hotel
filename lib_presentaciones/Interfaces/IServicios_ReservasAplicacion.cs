using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IServicios_ReservasPresentacion
    {
        Task<List<Servicios_Reservas>> Listar();
        Task<List<Servicios_Reservas>> PorCodigo(Servicios_Reservas? entidad);
        Task<Servicios_Reservas?> Guardar(Servicios_Reservas? entidad);
        Task<Servicios_Reservas?> Modificar(Servicios_Reservas? entidad);
        Task<Servicios_Reservas?> Borrar(Servicios_Reservas? entidad);
    }
}