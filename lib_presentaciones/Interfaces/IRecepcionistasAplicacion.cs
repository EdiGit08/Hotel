using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IRecepcionistasPresentacion
    {
        Task<List<Recepcionistas>> Listar();
        Task<List<Recepcionistas>> PorCarnet(Recepcionistas? entidad);
        Task<Recepcionistas?> Guardar(Recepcionistas? entidad);
        Task<Recepcionistas?> Modificar(Recepcionistas? entidad);
        Task<Recepcionistas?> Borrar(Recepcionistas? entidad);
    }
}