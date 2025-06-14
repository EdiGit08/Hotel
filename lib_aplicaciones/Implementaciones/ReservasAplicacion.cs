﻿using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ReservasAplicacion : IReservasAplicacion
    {
        private IConexion? IConexion = null;

        public ReservasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Reservas? Borrar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            GuardarAuditoria("Borrar Reservas");

            this.IConexion!.Reservas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Reservas? Guardar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            GuardarAuditoria("Crear Reservas");

            this.IConexion!.Reservas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Reservas> Listar()
        {
            return this.IConexion!.Reservas!
                .Include(x => x._Habitacion)
                .Include(y => y._Cliente)
                .Include(w => w._Recepcionista)
                .Take(20)
                .ToList();
        }

        public List<Reservas> PorCodigo(Reservas? entidad)
        {
            return this.IConexion!.Reservas!
                .Include(x => x._Habitacion)
                .Include(y => y._Cliente)
                .Include(w => w._Recepcionista)
                .Where(x => x.Codigo!.Contains(entidad!.Codigo!))
                .ToList();
        }

        public Reservas? Modificar(Reservas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            GuardarAuditoria("Modificar Reservas");

            var entry = this.IConexion!.Entry<Reservas>(entidad);
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
