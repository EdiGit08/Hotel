using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class RecepcionistasAplicacion : IRecepcionistasAplicacion
    {
        private IConexion? IConexion = null;

        public RecepcionistasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Recepcionistas? Borrar(Recepcionistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            GuardarAuditoria("Borrar Recepcionistas");

            this.IConexion!.Recepcionistas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Recepcionistas? Guardar(Recepcionistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            GuardarAuditoria("Crear Recepcionistas");

            this.IConexion!.Recepcionistas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Recepcionistas> Listar()
        {
            return this.IConexion!.Recepcionistas!.Take(20).ToList();
        }

        public List<Recepcionistas> PorCarnet(Recepcionistas? entidad)
        {
            return this.IConexion!.Recepcionistas!
                .Where(x => x.Carnet!.Contains(entidad!.Carnet!))
                .ToList();
        }

        public Recepcionistas? Modificar(Recepcionistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            GuardarAuditoria("Modificar Recepcionistas");

            var entry = this.IConexion!.Entry<Recepcionistas>(entidad);
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
