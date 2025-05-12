using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAuditoriasPagosPresentacion
    {
        Task<List<AuditoriasPagos>> Listar();
        Task<List<AuditoriasPagos>> PorCodigo(AuditoriasPagos? entidad);
        Task<AuditoriasPagos?> Guardar(AuditoriasPagos? entidad);
        Task<AuditoriasPagos?> Modificar(AuditoriasPagos? entidad);
        Task<AuditoriasPagos?> Borrar(AuditoriasPagos? entidad);
    }
}
