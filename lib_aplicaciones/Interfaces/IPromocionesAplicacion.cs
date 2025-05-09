using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IPromocionesAplicacion
    {
        void Configurar(string StringConexion);
        List<Promociones> PorCodigo(Promociones? entidad);
        List<Promociones> Listar();
        Promociones? Guardar(Promociones? entidad);
        Promociones? Modificar(Promociones? entidad);
        Promociones? Borrar(Promociones? entidad);
    }
}
