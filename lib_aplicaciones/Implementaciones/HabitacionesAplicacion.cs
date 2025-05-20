using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lib_aplicaciones.Implementaciones
{
    public class HabitacionesAplicacion : IHabitacionesAplicacion
    {
        private IConexion? IConexion = null;

        public HabitacionesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Habitaciones? Borrar(Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos
            GuardarAuditoria("Borrar Habitacion");

            this.IConexion!.Habitaciones!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Habitaciones? Guardar(Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos
            GuardarAuditoria("Crear Habitacion");

            this.IConexion!.Habitaciones!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Habitaciones> Listar()
        {
            return this.IConexion!.Habitaciones!.Take(20).ToList();
        }

        public List<Habitaciones> PorNombre(Habitaciones? entidad)
        {
            return this.IConexion!.Habitaciones!
                .Where(x => x.Nombre!.Contains(entidad!.Nombre!))
                .ToList();
        }

        public Habitaciones? Modificar(Habitaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos
            GuardarAuditoria("Modificar Habitacion");


            var entry = this.IConexion!.Entry<Habitaciones>(entidad);
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
                entidad.Codigo = "AHS" +  count.Next(100,999);
                entidad.Accion = accion;
                entidad.Fecha = DateTime.Now;
            };
            this.IConexion.Auditorias.Add(entidad);
        }
    }
}