using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class OpinionesAplicacion : IOpinionesAplicacion
    {
        private IConexion? IConexion = null;

        public OpinionesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Opiniones? Borrar(Opiniones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            GuardarAuditoria("Borrar Opiniones");


            this.IConexion!.Opiniones!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Opiniones? Guardar(Opiniones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos
            GuardarAuditoria("Crear Opiniones");


            this.IConexion!.Opiniones!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Opiniones> Listar()
        {
            return this.IConexion!.Opiniones!.Take(20).ToList();
        }

        public List<Opiniones> PorOpcion(Opiniones? entidad)
        {
            return this.IConexion!.Opiniones!
                .Where(x => x.Opcion!.Contains(entidad!.Opcion!))
                .ToList();
        }

        public Opiniones? Modificar(Opiniones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            GuardarAuditoria("Modificar Opiniones");


            var entry = this.IConexion!.Entry<Opiniones>(entidad);
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
            };
            this.IConexion.Auditorias!.Add(entidad);
        }
    }
}