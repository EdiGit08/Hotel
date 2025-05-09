using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IAuditoriasPagosAplicacion
    {
        void Configurar(string StringConexion);
        List<AuditoriasPagos> PorCodigo(AuditoriasPagos? entidad);
        List<AuditoriasPagos> Listar();
        AuditoriasPagos? Guardar(AuditoriasPagos? entidad);
        AuditoriasPagos? Modificar(AuditoriasPagos? entidad);
        AuditoriasPagos? Borrar(AuditoriasPagos? entidad);
    }

}
