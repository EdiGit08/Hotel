using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IPagosPresentacion
    {
        Task<List<Pagos>> Listar();
        Task<List<Pagos>> PorCodigo(Pagos? entidad);
        Task<Pagos?> Guardar(Pagos? entidad);
        Task<Pagos?> Modificar(Pagos? entidad);
        Task<Pagos?> Borrar(Pagos? entidad);
    }
}