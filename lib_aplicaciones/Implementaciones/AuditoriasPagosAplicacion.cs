using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AuditoriasPagosAplicacion : IAuditoriasPagosAplicacion
    {
        private IConexion? IConexion = null;

        public AuditoriasPagosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public AuditoriasPagos? Borrar(AuditoriasPagos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.AuditoriasPagos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public AuditoriasPagos? Guardar(AuditoriasPagos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.AuditoriasPagos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<AuditoriasPagos> Listar()
        {
            return this.IConexion!.AuditoriasPagos!.Take(20).ToList();
        }

        public List<AuditoriasPagos> PorCodigo(AuditoriasPagos? entidad)
        {
            return this.IConexion!.AuditoriasPagos!
                .Where(x => x.Codigo!.Contains(entidad!.Codigo!))
                .ToList();
        }

        public AuditoriasPagos? Modificar(AuditoriasPagos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<AuditoriasPagos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}