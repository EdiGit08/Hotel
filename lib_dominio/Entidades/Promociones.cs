

namespace lib_dominio.Entidades
{
    public class Promociones
    {

        public int Id { get; set; }
        public string? Codigo { get; set; }
        public short Descuento { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }

    }
}
