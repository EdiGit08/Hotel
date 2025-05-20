using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ServiciosAplicacion : IServiciosAplicacion
    {
        private IConexion? IConexion = null;

        public ServiciosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Servicios? Borrar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Servicios!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Servicios? Guardar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Servicios!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Servicios> Listar()
        {
            return this.IConexion!.Servicios!.Take(20).ToList();
        }

        public List<Servicios> PorTipo(Servicios? entidad)
        {
            return this.IConexion!.Servicios!
                .Where(x => x.Tipo!.Contains(entidad!.Tipo!))
                .ToList();
        }

        public Servicios? Modificar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Servicios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}