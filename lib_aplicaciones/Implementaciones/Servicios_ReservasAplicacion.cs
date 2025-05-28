using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

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

            GuardarAuditoria("Borrar Servicios_Reservas");

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

            GuardarAuditoria("Crear Servicios_Reservas");

            this.IConexion!.Servicios_Reservas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Servicios_Reservas> Listar()
        {
            return this.IConexion!.Servicios_Reservas!
                .Include(b => b._Servicio)
                .Include(v => v._Reserva)
                .Take(20)
                .ToList();
        }

        public List<Servicios_Reservas> PorCodigo(Servicios_Reservas? entidad)
        {
            return this.IConexion!.Servicios_Reservas!
                .Include(b => b._Servicio)
                .Include(v => v._Reserva)
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

            GuardarAuditoria("Modificar Servicios_Reservas");

            var entry = this.IConexion!.Entry<Servicios_Reservas>(entidad);
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