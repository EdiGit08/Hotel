using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AuditoriasAplicacion : IAuditoriasAplicacion
    {
        private IConexion? IConexion = null;

        public AuditoriasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public AuditoriasAplicacion()
        {
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Auditorias> Listar()
        {
            return this.IConexion!.Auditorias!.Take(20).ToList();
        }

        public List<Auditorias> PorCodigo(Auditorias? entidad)
        {
            return this.IConexion!.Auditorias!
                .Where(x => x.Codigo!.Contains(entidad!.Codigo!))
                .ToList();
        }
    }
}