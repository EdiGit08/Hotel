using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Servicios_ReservasAplicacion : IServicios_ReservasAplicacion
    {
        private IConexion? IConexion = null;

        public Servicios_ReservasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Servicios_Reservas? Borrar(Servicios_Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            this.IConexion!.Servicios_Reservas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Servicios_Reservas? Guardar(Servicios_Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            this.IConexion!.Servicios_Reservas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Servicios_Reservas> Listar()
        {
            return this.IConexion!.Servicios_Reservas!.Take(20).ToList();
        }

        public List<Servicios_Reservas> PorCodigo(Servicios_Reservas? entidad)
        {
            return this.IConexion!.Servicios_Reservas!
                .Where(x => x.Codigo!.Contains(entidad!.Codigo!))
                .ToList();
        }

        public Servicios_Reservas? Modificar(Servicios_Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            var entry = this.IConexion!.Entry<Servicios_Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}