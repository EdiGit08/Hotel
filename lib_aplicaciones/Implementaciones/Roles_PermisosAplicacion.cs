using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Roles_PermisosAplicacion : IRoles_PermisosAplicacion
    {
        private IConexion? IConexion = null;

        public Roles_PermisosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Roles_Permisos? Borrar(Roles_Permisos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos
            GuardarAuditoria("Borrar Roles_Permisos");

            this.IConexion!.Roles_Permisos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Roles_Permisos? Guardar(Roles_Permisos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos
            GuardarAuditoria("Crear Roles_Permisos");

            this.IConexion!.Roles_Permisos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Roles_Permisos> Listar()
        {
            return this.IConexion!.Roles_Permisos!.Take(20).ToList();
        }

        public List<Roles_Permisos> PorCodigo(Roles_Permisos? entidad)
        {
            return this.IConexion!.Roles_Permisos!
                .Where(x => x.Codigo!.Contains(entidad!.Codigo!))
                .ToList();
        }

        public Roles_Permisos? Modificar(Roles_Permisos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos
            GuardarAuditoria("Modificar Roles_Permisos");

            var entry = this.IConexion!.Entry<Roles_Permisos>(entidad);
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
