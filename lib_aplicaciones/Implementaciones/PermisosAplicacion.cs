using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PermisosAplicacion : IPermisosAplicacion
    {
        private IConexion? IConexion = null;

        public PermisosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Permisos? Borrar(Permisos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos
            GuardarAuditoria("Borrar Permisos");

            this.IConexion!.Permisos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Permisos? Guardar(Permisos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos
            GuardarAuditoria("Crear Permisos");

            this.IConexion!.Permisos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Permisos> Listar()
        {
            return this.IConexion!.Permisos!.Take(20).ToList();
        }

        public List<Permisos> PorTipo(Permisos? entidad)
        {
            return this.IConexion!.Permisos!
                .Where(x => x.Tipo!.Contains(entidad!.Tipo!))
                .ToList();
        }

        public Permisos? Modificar(Permisos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos
            GuardarAuditoria("Modificar Permisos");

            var entry = this.IConexion!.Entry<Permisos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
        public void GuardarAuditoria(string? accion)
        {

            Random count = new Random();

            var con = this.IConexion!.Auditorias!;
            var entidad = new Auditorias();
            {
                entidad.Codigo = "AHS" + count.Next(100, 999);
                entidad.Accion = accion;
                entidad.Fecha = DateTime.Now;
            }
            ;
            this.IConexion.Auditorias!.Add(entidad);
        }
    }
}
