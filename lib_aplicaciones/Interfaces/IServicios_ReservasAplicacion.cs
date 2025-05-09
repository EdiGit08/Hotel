using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IServicios_ReservasAplicacion
    {
        void Configurar(string StringConexion);
        List<Servicios_Reservas> PorCodigo(Servicios_Reservas? entidad);
        List<Servicios_Reservas> Listar();
        Servicios_Reservas? Guardar(Servicios_Reservas? entidad);
        Servicios_Reservas? Modificar(Servicios_Reservas? entidad);
        Servicios_Reservas? Borrar(Servicios_Reservas? entidad);
    }
}