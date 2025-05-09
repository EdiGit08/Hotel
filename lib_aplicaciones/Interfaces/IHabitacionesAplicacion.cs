using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IHabitacionesAplicacion
    {
        void Configurar(string StringConexion);
        List<Habitaciones> PorNombre(Habitaciones? entidad);
        List<Habitaciones> Listar();
        Habitaciones? Guardar(Habitaciones? entidad);
        Habitaciones? Modificar(Habitaciones? entidad);
        Habitaciones? Borrar(Habitaciones? entidad);
    }
}
