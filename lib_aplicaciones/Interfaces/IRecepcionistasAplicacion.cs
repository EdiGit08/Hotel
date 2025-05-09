using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IRecepcionistasAplicacion
    {
        void Configurar(string StringConexion);
        List<Recepcionistas> PorCarnet(Recepcionistas? entidad);
        List<Recepcionistas> Listar();
        Recepcionistas? Guardar(Recepcionistas? entidad);
        Recepcionistas? Modificar(Recepcionistas? entidad);
        Recepcionistas? Borrar(Recepcionistas? entidad);
    }
}