using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PromocionesAplicacion : IPromocionesAplicacion
    {
        private IConexion? IConexion = null;

        public PromocionesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Promociones? Borrar(Promociones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            GuardarAuditoria("Borrar Promociones");

            this.IConexion!.Promociones!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Promociones? Guardar(Promociones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Calculos

            GuardarAuditoria("Crear Promociones");


            this.IConexion!.Promociones!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Promociones> Listar()
        {
            return this.IConexion!.Promociones!.Take(20).ToList();
        }

        public List<Promociones> PorCodigo(Promociones? entidad)
        {
            return this.IConexion!.Promociones!
                .Where(x => x.Codigo!.Contains(entidad!.Codigo!))
                .ToList();
        }

        public Promociones? Modificar(Promociones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Calculos

            GuardarAuditoria("Modificar Promociones");


            var entry = this.IConexion!.Entry<Promociones>(entidad);
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
