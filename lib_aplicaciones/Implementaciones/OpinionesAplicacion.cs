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

            var entry = this.IConexion!.Entry<Opiniones>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}