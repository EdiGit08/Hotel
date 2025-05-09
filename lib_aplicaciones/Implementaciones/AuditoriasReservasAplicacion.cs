using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AuditoriasReservasAplicacion : IAuditoriasReservasAplicacion
    {
        private IConexion? IConexion = null;

        public AuditoriasReservasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public AuditoriasReservas? Borrar(AuditoriasReservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.AuditoriasReservas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public AuditoriasReservas? Guardar(AuditoriasReservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.AuditoriasReservas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<AuditoriasReservas> Listar()
        {
            return this.IConexion!.AuditoriasReservas!.Take(20).ToList();
        }

        public List<AuditoriasReservas> PorCodigo(AuditoriasReservas? entidad)
        {
            return this.IConexion!.AuditoriasReservas!
                .Where(x => x.Codigo!.Contains(entidad!.Codigo!))
                .ToList();
        }

        public AuditoriasReservas? Modificar(AuditoriasReservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<AuditoriasReservas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
