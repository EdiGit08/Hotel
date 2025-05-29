using lib_presentaciones;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ComunicacionesPrueba
    {
        private Comunicaciones _comunicaciones;

        public ComunicacionesPrueba()
        {

            _comunicaciones = new Comunicaciones("miServicio");

        }



        [TestMethod]

        public async Task Setup()
        {
            Assert.AreEqual(true, ConstruirUrlPrueba());

            var result = await EjecutarPrueba();
            Assert.IsFalse(result);

        }

        public async Task<bool> EjecutarPrueba()
        {

            var datos = new Dictionary<string, object>
            {
                { "Url", "http://localhost:5276/Clientes/Listar" },
                { "UrlToken", "http://localhost:5276/Token/Autenticar" }
            };

            var result = await _comunicaciones.Ejecutar(datos);

            Assert.IsFalse(result.ContainsKey("Error"));

            return result.ContainsKey("Error");
        }

        public bool ConstruirUrlPrueba()
        {
            var data = new Dictionary<string, object>();
            var metodo = "metodoPrueba";

            var result = _comunicaciones.ConstruirUrl(data, metodo);

            return result.ContainsKey("Url") && result.ContainsKey("UrlToken");
        }



    }
}
