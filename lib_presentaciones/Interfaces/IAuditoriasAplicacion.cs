using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAuditoriasPresentacion
    {
        Task<List<Auditorias>> Listar();
        Task<List<Auditorias>> PorCodigo(Auditorias? entidad);
    }
}
