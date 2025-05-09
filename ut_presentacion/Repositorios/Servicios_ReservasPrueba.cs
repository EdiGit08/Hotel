    using lib_dominio.Entidades;
    using lib_repositorios.Implementaciones;
    using lib_repositorios.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using ut_presentacion.Nucleo;

    namespace ut_presentacion.Repositorios
    {
        [TestClass]
        public class Servicios_ReservasPrueba
        {
            private readonly IConexion? iConexion;
            private List<Servicios_Reservas>? lista;
            private Servicios_Reservas? entidad;

            public Servicios_ReservasPrueba()
            {
                iConexion = new Conexion();
                iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            }

            [TestMethod]
            public void Ejecutar()
            {
                Assert.AreEqual(true, Guardar());
                Assert.AreEqual(true, Modificar());
                Assert.AreEqual(true, Listar());
                Assert.AreEqual(true, Borrar());
            }

            public bool Listar()
            {
                this.lista = this.iConexion!.Servicios_Reservas!.ToList();
                return lista.Count > 0;
            }

            public bool Guardar()
            {
                this.entidad = EntidadesNucleo.Servicios_Reservas()!;
                this.iConexion!.Servicios_Reservas!.Add(this.entidad);
                this.iConexion!.SaveChanges();
                return true;
            }

            public bool Modificar()
            {
                this.entidad!.Servicio = 1;

                var entry = this.iConexion!.Entry<Servicios_Reservas>(this.entidad);
                entry.State = EntityState.Modified;
                this.iConexion!.SaveChanges();
                return true;
            }

            public bool Borrar()
            {
                this.iConexion!.Servicios_Reservas!.Remove(this.entidad!);
                this.iConexion!.SaveChanges();
                return true;
            }
        }
    }
   

